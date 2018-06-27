using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class FieldDimensionModel : IFieldDimensionModel
    {
        [SerializeField]
        private Transform _gameFieldOrigin;

        [SerializeField]
        private float _fieldWidth;
        
        [SerializeField]
        private float _fieldHeight;

        [SerializeField]
        private uint _rows;
        
        [SerializeField]
        private uint _colls;
        
        
        
        public Transform GameFieldOrigin
        {
            get { return _gameFieldOrigin; }
            set { _gameFieldOrigin = value; }
        }

        public float FieldWidth
        {
            get { return _fieldWidth; }
            set { _fieldWidth = value; }
        }

        public float FieldHeight
        {
            get { return _fieldHeight; }
            set { _fieldHeight = value; }
        }

        public uint Rows
        {
            get { return _rows; }
            set { _rows = value; }
        }

        public uint Colls
        {
            get { return _colls; }
            set { _colls = value; }
        }

        public float HorizontalFactor
        {
            get { return _fieldWidth / _colls; }
        }
        
        public float VerticalFactor
        {
            get { return _fieldHeight / _rows; }
        }


        public Vector3 GetFieldWorldPosition(Vector2Int point)
        {
            return _gameFieldOrigin.TransformPoint(new Vector3(point.x * HorizontalFactor, point.y * VerticalFactor, 0));
        }

        
        //TODO: Сделать математически-верный способ преобразования
        public Vector2Int GetFieldPositionFromWorld(Vector3 worldPosition)
        {
            var plane = new Plane(_gameFieldOrigin.forward, _gameFieldOrigin.position);
            
            var closedPoint = plane.ClosestPointOnPlane(worldPosition) - _gameFieldOrigin.position;

            var x = closedPoint.x / HorizontalFactor;
            var y = closedPoint.y / VerticalFactor;
            
            return new Vector2Int((int) Math.Round(x), (int) Math.Round(y));
        }
    }
}