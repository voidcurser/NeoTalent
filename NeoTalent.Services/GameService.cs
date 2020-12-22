using System;
using System.Collections.Generic;
using System.Text;
using NeoTalent.Core.Domain;
using NeoTalent.Core.Exceptions;
using NeoTalent.Core.Interfaces;
using static NeoTalent.Core.Enumerators.CompassEnumerator;
using static NeoTalent.Core.Enumerators.GameObjectEnumerator;

namespace NeoTalent.Services
{
    public class GameService : IGameService
    {
        private int[,] _board;
        private int _rows;
        private int _columns;
        private Cat _cat;

        public int[,] CreateBoard(string[] BoardSettings)
        {
            try
            {
                var dimensions = BoardSettings[0].Split(" ");
                _rows = Convert.ToInt32(dimensions[0]);
                _columns = Convert.ToInt32(dimensions[1]);
                _board = new int[_rows, _columns];

                var dogs = BoardSettings[1].Split(" ");
                SetDogsOnBoard(dogs);

                var foodCoordinates = BoardSettings[2].Split(" ");
                SetFoodOnBoard(foodCoordinates);

                var catCoordinates = BoardSettings[3].Split(" ");
                SetCatOnBoard(catCoordinates);

                return _board;
            }
            catch (Exception ex)
            {

                throw new NeoTalentException(ex.Message);
            }
        }
        private void SetDogsOnBoard(string[] dogs)
        {

            foreach (var dog in dogs)
            {
                var dogCoordinates = dog.Split(",");
                var dogCoordinateX = Convert.ToInt32(dogCoordinates[0]);
                var dogCoordinateY = Convert.ToInt32(dogCoordinates[1]);
                _board[dogCoordinateX, dogCoordinateY] = (int)GameObject.Dog;
            }
        }
        private void SetFoodOnBoard(string[] food)
        {

            var foodPointX = Convert.ToInt32(food[0]);
            var foodPointY = Convert.ToInt32(food[1]);
            _board[foodPointX, foodPointY] = (int)GameObject.Food;

        }
        private void SetCatOnBoard(string[] catCoordinates)
        {

            if (Enum.TryParse(catCoordinates[2], out Compass currentDirection))
            {
                var catStartPosition = new Coordinates()
                {
                    X = Convert.ToInt32(catCoordinates[0]),
                    Y = Convert.ToInt32(catCoordinates[1]),
                    Dir = currentDirection
                };

                _cat = new Cat();
                _cat.StartPosition = catStartPosition;
                _cat.CurrentPosition = catStartPosition;
                _board[catStartPosition.X, catStartPosition.Y] = (int)GameObject.Cat;
            }
        }
        public List<string> MoveCat(string[] setOfMovements)
        {
            var result = new List<string>();
            foreach (var movements in setOfMovements)
            {
                var currentLineOfMovements = movements.Split(" ");
                foreach (var mov in currentLineOfMovements)
                {
                    if (result.Contains("Invalid Move"))
                        break;

                    switch (mov.ToString())
                    {
                        case "R":
                            RotateRightUpdateCurrentDirection();
                            break;
                        case "L":
                            RotateLeftUpdateCurrentDirection();
                            break;
                        case "M":
                            result.Add(Move());
                            break;
                        default :
                            throw new NeoTalentException("Movement Information on input file incorrect!");
                    }
                }
            }
            return result;
        }
        private void RotateRightUpdateCurrentDirection()
        {
            switch (_cat.CurrentPosition.Dir)
            {
                case Compass.N:
                    _cat.CurrentPosition.Dir = Compass.E;
                    break;
                case Compass.S:
                    _cat.CurrentPosition.Dir = Compass.W;
                    break;
                case Compass.E:
                    _cat.CurrentPosition.Dir = Compass.S;
                    break;
                case Compass.W:
                    _cat.CurrentPosition.Dir = Compass.N;
                    break;
                default:
                    break;
            }
        }
        private void RotateLeftUpdateCurrentDirection()
        {
            switch (_cat.CurrentPosition.Dir)
            {
                case Compass.N:
                    _cat.CurrentPosition.Dir = Compass.W;
                    break;
                case Compass.S:
                    _cat.CurrentPosition.Dir = Compass.E;
                    break;
                case Compass.E:
                    _cat.CurrentPosition.Dir = Compass.N;
                    break;
                case Compass.W:
                    _cat.CurrentPosition.Dir = Compass.S;
                    break;
                default:
                    break;
            }
        }
        private string Move()
        {
            switch (_cat.CurrentPosition.Dir)
            {
                case Compass.N:
                    if (_cat.CurrentPosition.X - 1 < 0)
                        return "Invalid Move";
                    else return MoveResult(_cat.CurrentPosition.X, _cat.CurrentPosition.Y, _cat.CurrentPosition.X -= 1, _cat.CurrentPosition.Y);
                case Compass.S:
                    if (_cat.CurrentPosition.X + 1 >= _rows)
                        return "Invalid Move";
                    else return MoveResult(_cat.CurrentPosition.X, _cat.CurrentPosition.Y, _cat.CurrentPosition.X += 1, _cat.CurrentPosition.Y);
                case Compass.E:
                    if (_cat.CurrentPosition.Y + 1 >= _columns)
                        return "Invalid Move";
                    else return MoveResult(_cat.CurrentPosition.X, _cat.CurrentPosition.Y, _cat.CurrentPosition.X, _cat.CurrentPosition.Y += 1);
                case Compass.W:
                    if (_cat.CurrentPosition.Y - 1 < 0)
                        return "Invalid Move";
                    else return MoveResult(_cat.CurrentPosition.X, _cat.CurrentPosition.Y, _cat.CurrentPosition.X, _cat.CurrentPosition.Y -= 1);
                default:
                    return "Invalid Direction";
            }
        }
        private string MoveResult(int beforeX, int beforeY, int currentX, int currentY)
        {
            if (_board[currentX, currentY] == (int)GameObject.Food)
                return "Success you reach the food!";
            else if (_board[currentX, currentY] == (int)GameObject.Dog)
                return "Oh boy it's a dog, its over, just runnn!";
            else
            {
                _board[beforeX, beforeY] = (int)GameObject.Empty;
                _board[currentX, currentY] = (int)GameObject.Cat;

                return "Still in Danger – the cat has not yet found the food or hit a dog";
            }
        }
    }
}
