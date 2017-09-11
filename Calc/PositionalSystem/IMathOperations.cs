
namespace Calc.PositionalSystem
{
    public interface IMathOperations<T>
        where T: class
    {
        T Add(T left);
        T Subtract(T left);
        T Multiply(T left);
        T Divide(T left);
        
    }
}
