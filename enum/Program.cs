
internal class Program
{
    static void Main()
    {
        try
        {
            int result = 10 / 0;  // ArithmeticException 발생
            Console.WriteLine("결과: " + result);
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("0으로 나눌 수 없습니다.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("예외가 발생했습니다: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("finally 블록이 실행되었습니다.");
        }


    }

}
