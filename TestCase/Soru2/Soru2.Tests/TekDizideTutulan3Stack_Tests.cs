using System;
using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace Soru2.Tests
{
    public class TekDizideTutulan3Stack_Tests
    {
        public enum TestStackItem
        {
            Alti = 6,
        }

        [Fact]
        public void Test1()
        {
            var tekDizideTutulan3Stack = new TekDizideTutulan3Stack();
            tekDizideTutulan3Stack.Push(0, 1);
            tekDizideTutulan3Stack.Push(0, 2);
            tekDizideTutulan3Stack.Push(0, 3);

            tekDizideTutulan3Stack.Push(1, "4");
            tekDizideTutulan3Stack.Push(1, "5");

            tekDizideTutulan3Stack.Push(2, TestStackItem.Alti);

            Should.Throw<IndexOutOfRangeException>(() => tekDizideTutulan3Stack.Push(3, 1))
                .Message.ShouldBe("İndex 2'den büyük olamaz");

            tekDizideTutulan3Stack.Pop(0).ShouldBe(3);
            tekDizideTutulan3Stack.Pop(1).ShouldBe("5");
            tekDizideTutulan3Stack.Pop(2).ShouldBe(TestStackItem.Alti);
            
            Should.Throw<IndexOutOfRangeException>(() => tekDizideTutulan3Stack.Pop(3))
                .Message.ShouldBe("İndex 2'den büyük olamaz");
            
            tekDizideTutulan3Stack.Pop(0).ShouldBe(2);
            tekDizideTutulan3Stack.Pop(1).ShouldBe("4");
            Should.Throw<InvalidOperationException>(() => tekDizideTutulan3Stack.Pop(2))
                .Message.ShouldBe("Stack Empty");
            
            tekDizideTutulan3Stack.Pop(0).ShouldBe(1);
            Should.Throw<InvalidOperationException>(() => tekDizideTutulan3Stack.Pop(1))
                .Message.ShouldBe("Stack Empty");
            Should.Throw<InvalidOperationException>(() => tekDizideTutulan3Stack.Pop(2))
                .Message.ShouldBe("Stack Empty");
            
            Should.Throw<InvalidOperationException>(() => tekDizideTutulan3Stack.Pop(0))
                .Message.ShouldBe("Stack Empty");
            Should.Throw<InvalidOperationException>(() => tekDizideTutulan3Stack.Pop(2))
                .Message.ShouldBe("Stack Empty");
            Should.Throw<InvalidOperationException>(() => tekDizideTutulan3Stack.Pop(2))
                .Message.ShouldBe("Stack Empty");
        }
    }
}