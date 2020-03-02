using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJ_Controls.Communication;
using System.Collections;

namespace WpfApplication1.Class
{
    class CClink
    {
        //CCLINK DEVICE MONITOR UTILITY 각 영역마다 최대 크기 참조함 가변될 수 있음
        const int MAX_X_COUNT = 8192;
        const int MAX_Y_COUNT = 8192;
        const int MAX_SB_COUNT = 512;
        const int MAX_SW_COUNT = 512;
        const int MAX_RAB_COUNT = 1535;
        const int MAX_WW_COUNT = 2047;
        const int MAX_WR_COUNT = 2047;
        const int MAX_SPB_COUNT = 32767;

        //직접접근으로 연결되지 않은 영역 접근시 오류발생, 그 떄문에 buffer메모리에 접속후 값을 읽음
        //CCLINK VER 2 사용가능 기기만 접근 가능
        //데이터 단위 word이므로 0~15 - 16384 , 16~31 - 16385 
        //16384 == 5 라면 --> X0==1 X1==0 X2==1  X3==0 X4==0 .....

        const int SPB_START_ADDRESS_BIT_X_VER2 = 16384;
        const int SPB_END_ADDRESS_BIT_X_VER2 = 16896;
        const int SPB_START_ADDRESS_BIT_Y_VER2 = 16896;
        const int SPB_END_ADDRESS_BIT_Y_VER2 = 17407;
        const int SPB_START_ADDRESS_WORD_WW_VER2 = 17408;
        const int SPB_END_ADDRESS_WORD_WW_VER2 = 19455;
        const int SPB_START_ADDRESS_WORD_WR_VER2 = 19456;
        const int SPB_END_ADDRESS_WORD_WR_VER2 = 21503;


        //VER 1
        const int SPB_START_ADDRESS_WORD_WW_VER1 = 480;
        const int SPB_END_ADDRESS_WORD_WW_VER1 = 735;
        const int SPB_START_ADDRESS_WORD_WR_VER1 = 736;
        const int SPB_END_ADDRESS_WORD_WR_VER1 = 1503;
        const int SPB_START_ADDRESS_BIT_SB_VER1 = 1504;
        const int SPB_END_ADDRESS_BIT_SB_VER1 = 1535;
        const int SPB_START_ADDRESS_WORD_SW_VER1 = 1536;
        const int SPB_END_ADDRESS_WORD_SW_VER1 = 2047;
        const int SPB_START_ADDRESS_WORD_RAB_VER1 = 2560;
        const int SPB_END_ADDRESS_WORD_RAB_VER1 = 4095;




        public bool[] bitXArray;
        public bool[] bitYArray;
        public bool[] bitSBArray;
        public short[] wordSWArray;
        public short[] wordRABArray;
        public short[] wordWwArray;
        public short[] wordWrArray;
        public short[] wordSPBArray;

        public delegate void MessageEventHandler(string msg);
        public event MessageEventHandler SomeEvent;

        COM_Melsec _melsec;





        public CClink()
        {
            bitXArray = new bool[MAX_X_COUNT];
            bitYArray = new bool[MAX_Y_COUNT];
            bitSBArray = new bool[MAX_SB_COUNT];
            wordSWArray = new short[MAX_SW_COUNT];
            wordRABArray = new short[MAX_RAB_COUNT];
            wordWwArray = new short[MAX_WW_COUNT];
            wordWrArray = new short[MAX_WR_COUNT];
            wordSPBArray = new short[MAX_SPB_COUNT];

            _melsec = new COM_Melsec();
            _melsec.MessageEvent += _melsec_MessageEvent;
        }


        public void SetValue(COM_Melsec.DeviceType type, int address, int value)
        {
            short startAddress = 0;
            short[] shotArray = wordSPBArray;
            
            short originValue = 0;
            int comparisontarget = 0;
            int target = 0;
            int inputValue = 0;
            bool[] bArray = bArray = new bool[16]; 

            switch (type)
            {
                case COM_Melsec.DeviceType.X:
                    if (value < 0 || value > 1) return;
                    
                    comparisontarget = address > 16 ? address % 16 : address;
                    bArray[comparisontarget] = value == 0 ? false : true;
                    target = getInt(bArray);
                    originValue = wordSPBArray[comparisontarget + SPB_END_ADDRESS_BIT_X_VER2];

                    inputValue = originValue| target;

                    shotArray[comparisontarget + SPB_END_ADDRESS_BIT_X_VER2] = (short)inputValue;
                    _melsec.SetWordEx(0, shotArray, shotArray.Length);

                    break;
                case COM_Melsec.DeviceType.Y:
                    if (value < 0 || value > 1) return;
                    comparisontarget = address > 16 ? address % 16 : address;
                    bArray[comparisontarget] = value == 0 ? false : true;
                    target = getInt(bArray);
                    originValue = wordSPBArray[comparisontarget + SPB_END_ADDRESS_BIT_X_VER2];

                    inputValue = originValue | target;

                    shotArray[comparisontarget + SPB_END_ADDRESS_BIT_X_VER2] = (short)inputValue;
                    _melsec.SetWordEx(0, shotArray, shotArray.Length);

                    break;
                case COM_Melsec.DeviceType.SB:
                    if (value < 0 || value > 1) return;
                    comparisontarget = address > 16 ? address % 16 : address;
                    bArray[comparisontarget] = value == 0 ? false : true;
                    target = getInt(bArray);
                    originValue = wordSPBArray[comparisontarget + SPB_END_ADDRESS_BIT_X_VER2];

                    inputValue = originValue | target;

                    shotArray[comparisontarget + SPB_END_ADDRESS_BIT_X_VER2] = (short)inputValue;
                    _melsec.SetWordEx(0, shotArray, shotArray.Length);
                    break;
                case COM_Melsec.DeviceType.SW:
                    break;
                case COM_Melsec.DeviceType.RAB:
                    break;
                case COM_Melsec.DeviceType.Ww:
                    break;
                case COM_Melsec.DeviceType.Wr:
                    break;
                case COM_Melsec.DeviceType.SPB:
                    break;
            }
        }

        int getInt(bool[] bArray)
        {
            int binary = 1;
            int retValue = 0;
            foreach (var i in bArray)
            {
                if (i == true)
                {
                    retValue += binary;
                }
                binary = binary << 1;
            }

            return retValue;
        }


        void _melsec_MessageEvent(string msg)
        {
            if (SomeEvent != null)
                SomeEvent(msg);

        }
       
        public void Open(int ChannelNumber)
        {
            if (_melsec.IsOpen) _melsec.Close();
            _melsec.DEVICE_CH = (short)ChannelNumber;
            _melsec.Open();
        }
        public bool isOpen()
        {
            return _melsec.IsOpen;
        }

        public void Refrash()
        {
            _melsec.DEVICETYPE_WORD = (short)COM_Melsec.DeviceType.SPB;
            //_melsec.GetWordEx(0, wordSPBArray, wordSPBArray.Length);

            bool[] bBuf;
            short[] sBuf = wordSPBArray;

            //shortArray --> boolArray
            bBuf = sBuf
    .SelectMany(s => Enumerable.Range(0, 16).Select(i => (s & (1 << i)) != 0))
    .ToArray();




            //BitXArray 세팅
            setBoolArray(bBuf, bitXArray, SPB_START_ADDRESS_BIT_X_VER2);

            //BitYArray 세팅
            setBoolArray(bBuf, bitYArray, SPB_START_ADDRESS_BIT_Y_VER2);

            //BitSBArray 세팅
            setBoolArray(bBuf, bitSBArray, SPB_START_ADDRESS_BIT_SB_VER1);

            //wordSWArray 세팅
            setWordArray(sBuf, wordSWArray, SPB_START_ADDRESS_WORD_SW_VER1);

            //wordRABArray 세팅
            setWordArray(sBuf, wordRABArray, SPB_START_ADDRESS_WORD_RAB_VER1);

            //wordWwArray 세팅
            setWordArray(sBuf, wordWwArray, SPB_START_ADDRESS_WORD_WW_VER2); //버전1 버전2 읽기가능 

            //wordWrArray 세팅
            setWordArray(sBuf, wordWrArray, SPB_START_ADDRESS_WORD_WR_VER2); //버전1 버전2 읽기가능

            //wordSPBArray 기존에 받아놨으므로 생략
        }

  

        void setBoolArray(bool[] originArray, bool[] targetArray, int startAddress)
        {
            int start = startAddress * 16;
            for (int i = 0; i < targetArray.Length; i++)
            {
                targetArray[i] = originArray[start++];
            }
        }

        void setWordArray(short[] originArray, short[] targetArray, int startAddress)
        {
            for (int i = 0; i < targetArray.Length; i++)
            {
                targetArray[i] = originArray[startAddress++];
            }

        }





    }
}
