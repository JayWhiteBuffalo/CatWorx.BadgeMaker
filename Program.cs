// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CatWorx.BadgeMaker
{
    class Program
    {
        
        async static Task Main(string[] args)
        {
            // List<Employee> employees = PeopleFetcher.GetEmployees();
            List<Employee> employees = await PeopleFetcher.GetFromApi();
            Util.PrintEmployees(employees);
            Util.MakeCSV(employees);
            await Util.MakeBadges(employees);
            
        }

        

    }
}