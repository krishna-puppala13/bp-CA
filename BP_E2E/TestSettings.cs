using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP_E2E
{
    public static class TestSettings
    {
        // You can override this with the BASE_URL environment variable if needed
        public static string BaseUrl =>
            Environment.GetEnvironmentVariable("BASE_URL")
            ?? "http://localhost:53135"; // change to http://localhost:5000 if that's what your app uses
    }
}
