//ocp - Open Closed Principle
using System;

namespace DesignPatterns.solid
{

    //Violaton: The violation of the OCP, are coming every time and changing code in main functionality.
    //Rather extend the functionality by without disturbing existing main functionality via Open-Closed Principle.


    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        small, medium, large
    }

    public class Product
    {

        public string Name;
        public Color color;
        public Size size;

        public Product(string name, Color col, Size siz)
        {
            Name = name;
            color = col;
            size = siz;
        }
    }

    //Here, it vioaltes the open-closed principle as we know the class need to open for extension, but closed for modifications.
    //So, every time adding new function into class breaks the open-closed principle
    //
    public class ProductFilter
    {
        //Product filter by the size
        public IEnumerable<Product> ProductFilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var p in products)
            {
                if (p.size == size)
                {
                    yield return p;
                }
            }
        }

        //Product filter by the Color
        public IEnumerable<Product> ProductFilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
            {
                if (p.color == color)
                {
                    yield return p;
                }
            }
        }

        //Product filter by size and color
        public IEnumerable<Product> ProductFilterByColorAndSize(IEnumerable<Product> products, Color color, Size size)
        {
            foreach (var p in products)
            {
                if (p.color == color && p.size == size)
                {
                    yield return p;
                }
            }
        }

    }

    //Implementing a code, follows the Open-Closed Principle

    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }


    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }


    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;
        public ColorSpecification(Color color) {
            this.color = color;
        }
        public bool IsSatisfied(Product p)
        {
            return p.color == color;
        }
        
    }

    //Based on the ISpecification we can just extend it to add SizeSpecification, simple inheritance.
    //In same-way can able to extend the features using the ISpecification.
    //Based on call from the main function, over here.
    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;

        public SizeSpecification(Size size)
        {
            this.size = size;
        }

        public bool IsSatisfied(Product p)
        {
            return p.size == size;
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
            {
                if (spec.IsSatisfied(i))
                {
                    yield return i;
                }
            }
        }
    }


    public class OpenClosedExample
    {
        public static void ExecutionOCP()
        {
            Console.WriteLine("Entered into OCP");

            // int[] arr = new int[] { 1, 2, 3, 4, 5 };
            // IEnumerable<int> newArr = arr as IEnumerable<int>;

            // foreach (var item in newArr)
            // {
            //     Console.WriteLine(item);
            // }

            var apple = new Product("Apple", Color.Red, Size.small);
            var tree = new Product("Tree", Color.Green, Size.medium);
            var house = new Product("House", Color.Blue, Size.large);


            Console.WriteLine("Method by vioaliting Open-closed Principle: ");
            Product[] p = { apple, tree, house };

            var pf = new ProductFilter();

            foreach (var ps in pf.ProductFilterByColor(p, Color.Green))
            {
                Console.WriteLine($"product: {ps.Name}");
            }

            Console.WriteLine("Method by Following Open-Closed Principle: ");

            var bf = new BetterFilter();

            foreach (var ps in bf.Filter(p, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($"product: {ps.Name}");
            }

            //Todo: Add the combined specification by extending the functionality for both Color and Size

        }
    }
}