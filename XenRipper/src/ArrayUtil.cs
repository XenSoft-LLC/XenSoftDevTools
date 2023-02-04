using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XenRipper.src
{
    public static class ArrayUtil<T>
    {
        public static T[][] sliceObjectArray(T[] objectArray, int sliceSize)
        {
            int objectCount = 0;
            T[][] splitArray = new T[objectArray.Length / sliceSize][];
            for (var i = 0; i < objectArray.Length; i += sliceSize)
            {
                T[] chunk = objectArray.Skip(i).Take(sliceSize).ToArray();
                splitArray[objectCount] = chunk;
                objectCount++;
            }
            return splitArray;
        }
    }
}
