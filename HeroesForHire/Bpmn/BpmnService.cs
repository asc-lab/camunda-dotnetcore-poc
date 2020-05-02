using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Camunda.Api.Client;
using Camunda.Api.Client.Deployment;
using Camunda.Api.Client.ProcessDefinition;
using Camunda.Api.Client.ProcessInstance;
using Camunda.Api.Client.UserTask;
using HeroesForHire.Controllers.Dtos;
using HeroesForHire.Domain;

namespace HeroesForHire
{
    public class BpmnService
    {
        private readonly CamundaClient camunda;

        public BpmnService()
        {
            this.camunda = CamundaClient.Create("http://localhost:8080/rest/engine/default");
        }

        public async Task DeployProcessDefinition()
        {
            var bpmnResourceStream = this.GetType().Assembly.GetManifestResourceStream("HeroesForHire.Bpmn.hire-heroes.bpmn");
            try
            {

                await camunda.Deployments.Create("HireHeroes Deployment",
                    new ResourceDataContent(bpmnResourceStream, "hire-heroes.bpmn"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<string> StartProcessFor(Order order)
        {
            var processParams = new StartProcessInstance()
                .SetVariable("orderId", VariableValue.FromObject(order.Id.Value.ToString()))
                .SetVariable("orderStatus", VariableValue.FromObject(order.Status.ToString()))
                .SetVariable("customerCode", VariableValue.FromObject(order.Customer.Code));
            var processStartResult = await 
                camunda.ProcessDefinitions.ByKey("Process_Hire_Hero").StartProcessInstance(processParams);

            return processStartResult.Id;
        }

        public async Task<List<UserTaskInfo>> GetTasksForCandidateGroup(string group, string user)
        {
            var groupTaskQuery = new TaskQuery
            {
                ProcessDefinitionKeys = { "Process_Hire_Hero" },
                CandidateGroup = group
            };
            var groupTasks = await camunda.UserTasks.Query(groupTaskQuery).List();

            if (user != null)
            {
                var userTaskQuery = new TaskQuery
                {
                    ProcessDefinitionKeys = { "Process_Hire_Hero" },
                    Assignee = user
                };
                var userTasks = await camunda.UserTasks.Query(userTaskQuery).List();

                groupTasks.AddRange(userTasks);    
            }
            
            return groupTasks;
        }

        public async Task<UserTaskInfo> ClaimTask(string taskId, string user)
        {
            await camunda.UserTasks[taskId].Claim(user);
            
            var task = await camunda.UserTasks[taskId].Get();
            
            return task;
        }
        
        public async Task<UserTaskInfo> CompleteTask(string taskId, Order order)
        {
            var task = await camunda.UserTasks[taskId].Get();

            var completeTask = new CompleteTask()
                .SetVariable("orderStatus", VariableValue.FromObject(order.Status.ToString()));
                
            await camunda.UserTasks[taskId].Complete(completeTask);
            
            return task;
        }

        public async Task CleanupProcessInstances()
        {
            var instances = await camunda.ProcessInstances
                .Query(new ProcessInstanceQuery
                {
                    ProcessDefinitionKey = "Process_Hire_Hero"
                })
                .List();

            if (instances.Count>0)
            {
                await camunda.ProcessInstances.Delete(new DeleteProcessInstances
                {
                    ProcessInstanceIds = instances.Select(i=>i.Id).ToList()
                });
            }
        }
    }
}