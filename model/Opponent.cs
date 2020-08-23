using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.model
{
    public class Opponent
    {
        private static List<int> _blackList = new List<int>();
        private Random _random = new Random();
        private int _rndInd;
        private int _numOfColumns = 10; 
        private static int _numOfSpots = 100;
        private IList<int> _availableIndexes = new List<int>();
        private List<int> _targetIndexes = new List<int>();
        private List<int> _potentialAims = new List<int>();
        private List<int> _collectionOfClearedIndexes = new List<int>();
        public Opponent()
        {
            FillAvailableIndexes();
        }

        private void FillAvailableIndexes()
        {
            int numOfIndexes = _numOfColumns*_numOfColumns;

            for(int i = 0; i < numOfIndexes; i++)
            {
                _availableIndexes.Add(i);
            }
        }
        public int MakeGuess()
        {
            _potentialAims.Clear();
            if(!IsTargetIndicated())
            {
                System.Console.WriteLine("Target is NOT indicated");
                _rndInd = RandomGuess();
                RemoveFromAvailableIndexes(_rndInd);
                return _rndInd;
            }
            else
            if(_targetIndexes.Count == 1)
            {
                System.Console.WriteLine("Target count == 1");
                CheckHorizontalOptions();
                CheckUprightOptions(); 

                WritePotentialAims(_potentialAims);

                _rndInd = _potentialAims[_random.Next(0, _potentialAims.Count)];
                RemoveFromAvailableIndexes(_rndInd);
                return _rndInd;
            }
            else
            {
                _targetIndexes.Sort();
                uint diff = Convert.ToUInt32(_targetIndexes[1] - _targetIndexes[0]);
                if(CheckShipPosition() == "Horizontal") CheckHorizontalOptions();
                if(CheckShipPosition() == "Upright") CheckUprightOptions();

                WritePotentialAims(_potentialAims);

                _rndInd = _potentialAims[_random.Next(0, _potentialAims.Count)];
                RemoveFromAvailableIndexes(_rndInd);
                return _rndInd;
            }
        }

        private void WritePotentialAims(List<int> _potentialAims)
        {
            StringBuilder strBldr = new StringBuilder("PotentialTargets: ");

            for(int i=0; i<_potentialAims.Count; i++)
            {
                strBldr.Append(_potentialAims[i] + " ");
            } 
            System.Console.WriteLine(strBldr);
        }

        private string CheckShipPosition()
        {
            uint diff = Convert.ToUInt32(_targetIndexes[1] - _targetIndexes[0]);
            if(diff == 1) return "Horizontal";
            if(diff == _numOfColumns) return "Upright";
            else
                return "Position not defined";
        }

        private bool IsTargetIndicated()
        {
            return _targetIndexes.Count > 0 ? true : false;
        }

        public void ShotResult(string shotResult)
        {
            if(shotResult == "Destroyed")
            {
                _targetIndexes.Add(_rndInd);
                ClearAllAvailableIndexesAroundTheShip();
            }

            if(shotResult == "Hit")
            {
                _targetIndexes.Add(_rndInd);
            }
        }

        private void CheckUprightOptions()
        {
           _targetIndexes.Sort();
            int firstId = _targetIndexes[0];
            int lastId = _targetIndexes[_targetIndexes.Count - 1];

            if(firstId - _numOfColumns >=  0 && SpotAvailable(firstId - _numOfColumns))
            {
                int newId = firstId - _numOfColumns;
                _potentialAims.Add(newId);
            }

            if(lastId + _numOfColumns <=  _numOfSpots - 1 && SpotAvailable(lastId + _numOfColumns))
            {
                int newId = lastId + _numOfColumns;
                _potentialAims.Add(newId);
            }
        }

        private void CheckHorizontalOptions()
        {
            _targetIndexes.Sort();
            int firstId = _targetIndexes[0];
            int lastId = _targetIndexes[_targetIndexes.Count - 1];

            if(firstId - 1 >=  GetLeftBorder(firstId) && SpotAvailable(firstId - 1))
            {
                int newId = firstId - 1;
                _potentialAims.Add(newId);
            }

            if(lastId + 1 <=  GetRightBorder(lastId) && SpotAvailable(lastId + 1))
            {
                int newId = lastId + 1;
                _potentialAims.Add(newId);
            }
        }

        private int GetLeftBorder(int id)
        {
            decimal dec = id / _numOfColumns;
            int ret = (int)Math.Floor(dec) * _numOfColumns;
            return ret;
        }

        private int GetRightBorder(int id)
        {
            decimal dec = id / _numOfColumns;
            int ret = (int)Math.Floor(dec) * _numOfColumns + (_numOfColumns - 1);
            return ret;
        }

        private int RandomGuess()
        {
            return _availableIndexes[_random.Next(_availableIndexes.Count)];
        }

        private bool SpotAvailable(int id)
        {
            return _availableIndexes.Contains(id);
        }

        private void RemoveFromAvailableIndexes(int id)
        {
            _availableIndexes.Remove(id);
        }

        private void ClearAllAvailableIndexesAroundTheShip()
        {
            _targetIndexes.Sort();
            _collectionOfClearedIndexes.Clear();
            if(CheckShipPosition() == "Horizontal")
            {
                
                if(_targetIndexes[0] - 1 >=  GetLeftBorder(_targetIndexes[0]))
                {
                    int newId = _targetIndexes[0] - 1;
                    _targetIndexes.Add(newId);
                    _targetIndexes.Sort();
                }

                if(_targetIndexes[_targetIndexes.Count - 1] + 1 <=  GetRightBorder(_targetIndexes[_targetIndexes.Count - 1]))
                {
                    int newId = _targetIndexes[_targetIndexes.Count - 1] + 1;
                    _targetIndexes.Add(newId);
                    _targetIndexes.Sort();
                }


                for(int i=0; i < _targetIndexes.Count; i++)
                {
                    if(_targetIndexes[i] - _numOfColumns >=  0 && SpotAvailable(_targetIndexes[i] - _numOfColumns))
                    {
                        int newId = _targetIndexes[i] - _numOfColumns;
                        _collectionOfClearedIndexes.Add(newId);
                        RemoveFromAvailableIndexes(newId);

                        if(SpotAvailable(_targetIndexes[i]))
                        {
                            _collectionOfClearedIndexes.Add(_targetIndexes[i]);
                            RemoveFromAvailableIndexes(_targetIndexes[i]);
                        }
                    }

                    if(_targetIndexes[i] + _numOfColumns <=  _numOfSpots - 1 && SpotAvailable(_targetIndexes[i] + _numOfColumns))
                    {
                        int newId = _targetIndexes[i] + _numOfColumns;
                        _collectionOfClearedIndexes.Add(newId);
                        RemoveFromAvailableIndexes(newId);

                        if(SpotAvailable(_targetIndexes[i]))
                        {
                            _collectionOfClearedIndexes.Add(_targetIndexes[i]);
                            RemoveFromAvailableIndexes(_targetIndexes[i]);
                        }
                    }
                }
            }

            if(CheckShipPosition() == "Upright")
            {
                if(_targetIndexes[0] - _numOfColumns >=  0)
                {
                    int newId = _targetIndexes[0] - _numOfColumns;
                    _targetIndexes.Add(newId);

                    _targetIndexes.Sort();
                }

                if(_targetIndexes[_targetIndexes.Count - 1] + _numOfColumns <=  _numOfSpots - 1)
                {
                    int newId = _targetIndexes[_targetIndexes.Count - 1] + _numOfColumns;
                    _targetIndexes.Add(newId);
                    _targetIndexes.Sort();
                }

                for(int i=0; i < _targetIndexes.Count; i++)
                {
                    if(_targetIndexes[i] - 1 >=  GetLeftBorder(_targetIndexes[i]) && SpotAvailable(_targetIndexes[i] - 1))
                    {
                        int newId = _targetIndexes[i] - 1;
                        _collectionOfClearedIndexes.Add(newId);
                        RemoveFromAvailableIndexes(newId);

                        if(SpotAvailable(_targetIndexes[i]))
                        {
                            _collectionOfClearedIndexes.Add(_targetIndexes[i]);
                            RemoveFromAvailableIndexes(_targetIndexes[i]);
                        }
                    }

                    if(_targetIndexes[i] + 1 <=  GetRightBorder(_targetIndexes[i]) && SpotAvailable(_targetIndexes[i] + 1))
                    {
                        int newId = _targetIndexes[i] + 1;
                        _collectionOfClearedIndexes.Add(newId);
                        RemoveFromAvailableIndexes(newId);

                        if(SpotAvailable(_targetIndexes[i]))
                        {
                            _collectionOfClearedIndexes.Add(_targetIndexes[i]);
                            RemoveFromAvailableIndexes(_targetIndexes[i]);
                        }
                    }
                }
                    
            }
            
            _targetIndexes.Clear();

            System.Console.WriteLine("---CLEARED INDEXES---");
            StringBuilder strBldr = new StringBuilder();
            for(int i = 0; i < _collectionOfClearedIndexes.Count; i++)
            {
                strBldr.Append(_collectionOfClearedIndexes[i]  + " ");
            }
            System.Console.WriteLine(strBldr);
        }

        public List<int> GetClearedIndexesAroundTheShip()
        {
            return _collectionOfClearedIndexes;
        }

    }
}