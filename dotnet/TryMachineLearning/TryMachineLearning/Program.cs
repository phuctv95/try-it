using System;
using TryMachineLearningML.Model;

namespace TryMachineLearning
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new ModelInput
            {
                Vendor_id = "CMT",
                Rate_code = 1,
                Passenger_count = 1,
                Trip_distance = 3.8f,
                Payment_type = "CRD"
            };
            var result = ConsumeModel.Predict(input);
            Console.WriteLine(result.Score);
        }
    }
}
