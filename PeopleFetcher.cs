using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO.Pipes;

namespace CatWorx.BadgeMaker
{
    class PeopleFetcher
    {
        static public List<Employee> GetEmployees(){
            List<Employee> employees = new List<Employee>();
            while (true)
            {
                Console.WriteLine("Please enter a name: ");
                string firstName = Console.ReadLine() ?? "";
                if (firstName == ""){
                    break;
                }

                Console.WriteLine("Enter last name: ");
                string lastName = Console.ReadLine() ?? "";

                Console.WriteLine("Enter ID: ");
                int id = Int32.Parse(Console.ReadLine() ?? "");

                Console.WriteLine("Enter Photo URL: ");
                string photoUrl = Console.ReadLine() ?? "";

                Employee currentEmployee = new Employee(firstName, lastName, id, photoUrl);
                employees.Add(currentEmployee);
            }
            return employees;
        }

        async static public Task<List<Employee>> GetFromApi()
        {
            List<Employee> employees = new List<Employee>();

            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync("https://randomuser.me/api/?results=10&nat=us&inc=name,id,picture");
                JObject json = JObject.Parse(response);

                foreach (JToken person in json.SelectToken("results"))
                {
                    Employee employee = new Employee
                    (
                        person.SelectToken("name.first").ToString(),
                        person.SelectToken("name.last").ToString(),
                        Int32.Parse(person.SelectToken("id.value").ToString().Replace("-", "")),
                        person.SelectToken("picture.large").ToString()
                    );
                    employees.Add(employee);
                }
            return employees;
            }
            // private static HttpClient sharedClient = new()
            // {
            //     BaseAddress = new Uri("https://randomuser.me/api/?results=10&nat=us&inc=name,id,picture")
                
            //      HttpClient data =sharedClient.GetStreamAsync(BaseAddress)
            // };

        }
    };
};