using BenchmarkDotNet.Attributes;
using ECommerce.Benchmark.RestAPIs;

namespace ECommerce.Benchmark.APIsBenchmarks
{
    [HtmlExporter]
    public class ColorApiBenchmarks
    {
        [Params(100, 200)]
        public int IterationCount;

        private readonly ColorsController _colorsController = new ColorsController();

        [Benchmark]
        public async Task RestGetSmallPayloadAsync()
        {
            for (int i = 0; i < IterationCount; i++)
            {
                await _colorsController.GetAllAsync();
            }
        }
    }
}
