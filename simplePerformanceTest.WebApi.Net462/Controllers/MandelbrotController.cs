using System.Web.Http;

namespace simplePerformanceTest.WebApi.Net462.Controllers
{
    public class MandelbrotController : ApiController
    {
        public struct Pixel
        {
            public byte Red;

            public byte Green;

            public byte Blue;
        }

        [HttpGet]
        [Route("mandelbrot")]
        public Pixel[,] Render(double xMin = -2.1, double xMax = 0.9, double yMin = -1, double yMax = 1, int width=320, int height=200, int numberOfIterations=100)
        {
            var xScale = (xMax - xMin) / width;
            var yScale = (yMax - yMin) / height;
            Pixel[,] pixels = new Pixel[width,height];

            for (int sx = 0; sx < width; sx++)
            {
                for (int sy = 0; sy < height; sy++)
                {
                    double scaledX = sx * xScale + xMin;
                    double scaledY = sy * yScale + yMin;

                    int iteration = 0;
                    double x = 0;
                    double y = 0;
                    while ((x * x + y * y) <= (2 * 2) && iteration < numberOfIterations)
                    {
                        double xTemp = x * x - y * y + scaledX;
                        y = 2 * x * y + scaledY;
                        x = xTemp;

                        iteration = iteration + 1;
                    }

                    if (iteration == numberOfIterations)
                    {
                        pixels[sx, sy] = new Pixel
                        {
                            Red = 0,
                            Green = 0,
                            Blue = 0
                        };
                    }
                    else
                    {
                        Pixel pixel = GetPixel(iteration, numberOfIterations);
                        pixels[sx, sy] = pixel;
                    }
                }
            }

            return pixels;
        }

        private Pixel GetPixel(int iteration, int numberOfIterations)
        {
            var ratio = (double)iteration / (double)numberOfIterations;
            var red = 0.0;
            var green = 0.0;
            var blue = 0.0;

            if ((ratio >= 0) && (ratio < 0.125))
            {
                red = red = (ratio / 0.125) * (512) + 0.5;
                green = 0;
                blue = 0;
            }

            if ((ratio >= 0.125) && (ratio < 0.250))
            {
                red = 255;
                green = (((ratio - 0.125) / 0.125) * (512) + 0.5);
                blue = 0;
            }

            if ((ratio >= 0.250) && (ratio < 0.375))
            {
                red = ((1.0 - ((ratio - 0.250) / 0.125)) * (512) + 0.5);
                green = 255;
                blue = 0;
            }

            if ((ratio >= 0.375) && (ratio < 0.500))
            {
                red = 0;
                green = 255;
                blue = (((ratio - 0.375) / 0.125) * (512) + 0.5);
            }

            if ((ratio >= 0.500) && (ratio < 0.625))
            {
                red = 0;
                green = ((1.0 - ((ratio - 0.500) / 0.125)) * (512) + 0.5);
                blue = 255;
            }

            if ((ratio >= 0.625) && (ratio < 0.750))
            {
                red = (((ratio - 0.625) / 0.125) * (512) + 0.5);
                green = 0;
                blue = 255;
            }

            if ((ratio >= 0.750) && (ratio < 0.875))
            {
                red = 255;
                green = (((ratio - 0.750) / 0.125) * (512) + 0.5);
                blue = 255;
            }

            if ((ratio >= 0.875) && (ratio <= 1.000))
            {
                red = ((1.0 - ((ratio - 0.875) / 0.125)) * (512) + 0.5);
                green = ((1.0 - ((ratio - 0.875) / 0.125)) * (512) + 0.5);
                blue = ((1.0 - ((ratio - 0.875) / 0.125)) * (512) + 0.5);
            }

            return new Pixel()
            {
                Red = (byte) red,
                Green = (byte) green,
                Blue = (byte) blue
            };
        }

    }
}
