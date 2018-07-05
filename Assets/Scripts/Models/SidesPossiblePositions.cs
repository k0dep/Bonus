using UnityEngine;

namespace Models
{
    public struct SidesPossiblePositions
    {
        public Vector2Int? Right { get; set; }
        public Vector2Int? Left { get; set; }

        public SidesPossiblePositions(Vector2Int? right, Vector2Int? left)
        {
            Right = right;
            Left = left;
        }
    }
}