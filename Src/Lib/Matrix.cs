using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc2020.Lib
{
    public sealed class Matrix<T> where T : IComparable<T>
    {
        public int Height { get; }
        public int Width { get; }
        public List<T> Values { get; }

        public Matrix(int height, int width, T defaultValue) :
            this(height, width, Enumerable.Repeat(defaultValue, height * width))
        {
        }

        public Matrix(int height, int width, IEnumerable<T> values)
        {
            Height = height;
            Width = width;
            Values = values.ToList();
            if (Values.Count != height * width)
            {
                throw new ArgumentException("Invalid number of values");
            }
        }

        public Matrix<T> Clone() => new Matrix<T>(Height, Width, Values.ToList());

        public T this[int y, int x]
        {
            get => Values[IndexOf(y, x)];
            set => Values[IndexOf(y, x)] = value;
        }

        private int IndexOf(int y, int x)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Width)
            {
                throw new ArgumentException("Invalid coordinate");
            }

            return y * Width + x;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            for (var y = 0; y < Height; y ++)
            {
                for (var x = 0; x < Width; x ++)
                {
                    result.Append(this[y, x]);
                }

                result.Append('\n');
            }

            return result.ToString();
        }

        private bool Equals(Matrix<T> other)
        {
            return Height == other.Height &&
                   Width == other.Width &&
                   Values.Zip(other.Values).All(tup => Equals(tup.First, tup.Second));
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Matrix<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Height, Width, Values);
        }
    }
}
