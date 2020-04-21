using System;
using System.Threading.Tasks;
using Camunda.Api.Client;
using Camunda.Api.Client.Deployment;
using Camunda.Api.Client.ProcessDefinition;
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
    }
}