using System;
using UnityEngine;

namespace TadPoleFramework.Utility
{
    [Serializable]
    public class MatrixId
    {
        [field: SerializeField] public int RowId { get; private set; }

        [field: SerializeField] public int ColId { get; private set; }

        public MatrixId(int rowId, int colId)
        {
            RowId = rowId;
            ColId = colId;
        }

        public static MatrixId operator +(MatrixId matrixId1, MatrixId matrixId2)
        {
            return new MatrixId(matrixId1.RowId + matrixId2.RowId
                , matrixId1.ColId + matrixId2.ColId);
        }

        public static MatrixId operator *(MatrixId matrixId, int multiplier)
        {
            return new MatrixId(matrixId.RowId * multiplier, matrixId.ColId * multiplier);
        }
        
        public static bool operator ==(MatrixId matrixId1, MatrixId matrixId2)
        {
            // return (matrixId1 == matrixId2);
            return (matrixId1.RowId == matrixId2.RowId && matrixId1.ColId == matrixId2.ColId)
                ? true
                : false;
        }
        //
        public static bool operator !=(MatrixId matrixId1, MatrixId matrixId2)
        {
            return (matrixId1.RowId == matrixId2.RowId && matrixId1.ColId == matrixId2.ColId)
                ? true
                : false;
        }

        public override string ToString()
        {
            return $"[{RowId},{ColId}]";
        }
    }
}