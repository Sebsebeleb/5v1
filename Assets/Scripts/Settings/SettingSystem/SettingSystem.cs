/*using UnityEngine;

namespace Settings
{
    public class SettingSystem
    {

    }


    public class SettingsEntry<T>
    {

        private T _value;
        private bool Initalized = false;

        public void Setvalue(T val)
        {
            _value = val;
        }

        public T GetValue()
        {
            return _value;
        }

        private abstract void LoadValue();
    }

    public class IntValue
    {
        private int _low;
        private int _high;
        private int _value;
		private string _identifier;


        public IntValue(string identifier, int low, int high)
        {
            _low = low;
            _high = high;

			ClampMe();
        }

        public IntValue(string identifier, int low, int high, int current)
        {
            _value = current;
            _low = low;
            _high = high;

			ClampMe();
        }

		public void SaveValue(){
			PlayerPrefs.SetInt(_identifier, _value);
		}

		public void LoadValue(){
			_value = PlayerPrefs.GetInt(_identifier);
			ClampMe();
		}


        private int ClampMe()
        {
            if (_value > _high)
            {
                return _high;
            }
            else if (_value < _low)
            {
                return _low;
            }

            else
            {
                return _value;
            }
        }
    }

}*/