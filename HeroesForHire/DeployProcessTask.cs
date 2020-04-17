using System;
using System.Threading.Tasks;
using Camunda.Api.Client;
using Camunda.Api.Client.Deployment;
using System.IO;
using System.Reflection;

namespace HeroesForHire
{
    public class DeployProcessTask
    {
        public async Task Execute()
        {
            var camunda = CamundaClient.Create("http://localhost:32769/engine-rest");

            var assembly = Assembly.GetEntryAssembly();
            var bpmnResStream = this.GetType().Assembly.GetManifestResourceStream("HeroesForHire.Bpmn.hire-heroes.bpmn");

            try
            {

                await camunda.Deployments.Create("HireHeroes Deployment",
                    new ResourceDataContent(bpmnResStream, "hire-heroes.bpmn"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}