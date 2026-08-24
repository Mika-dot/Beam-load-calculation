namespace EngineeringCalculator.Mathematics;

internal static class LinearSystem
{
    public static double[] Solve(double[,] matrix, double[] right)
    {
        var n = right.Length;
        if (matrix.GetLength(0) != n || matrix.GetLength(1) != n)
            throw new ArgumentException("Матрица системы должна быть квадратной и согласованной с правой частью.");

        var a = (double[,])matrix.Clone();
        var b = (double[])right.Clone();

        for (var column = 0; column < n; column++)
        {
            var pivot = column;
            for (var row = column + 1; row < n; row++)
                if (Math.Abs(a[row, column]) > Math.Abs(a[pivot, column])) pivot = row;

            var scale = 0d;
            for (var j = column; j < n; j++) scale = Math.Max(scale, Math.Abs(a[pivot, j]));
            if (scale == 0 || Math.Abs(a[pivot, column]) < 1e-12 * scale)
                throw new InvalidOperationException("Расчётная схема геометрически изменяема или имеет недостаточно закреплений.");

            if (pivot != column)
            {
                for (var j = column; j < n; j++) (a[column, j], a[pivot, j]) = (a[pivot, j], a[column, j]);
                (b[column], b[pivot]) = (b[pivot], b[column]);
            }

            for (var row = column + 1; row < n; row++)
            {
                var factor = a[row, column] / a[column, column];
                a[row, column] = 0;
                for (var j = column + 1; j < n; j++) a[row, j] -= factor * a[column, j];
                b[row] -= factor * b[column];
            }
        }

        var x = new double[n];
        for (var row = n - 1; row >= 0; row--)
        {
            var value = b[row];
            for (var j = row + 1; j < n; j++) value -= a[row, j] * x[j];
            x[row] = value / a[row, row];
        }
        return x;
    }
}
