using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.WPF.ViewModels
{
    public class SpeakerVolumeViewModel : VolumeViewModel
    {
        public SpeakerVolumeViewModel() : base(NativeMethods.Dll_GetVolExport, NativeMethods.Dll_SetVolExport) { }
    }

    public class RingVolumeViewModel : VolumeViewModel
    {
        public RingVolumeViewModel() : base(NativeMethods.Dll_GetRingVol, NativeMethods.Dll_SetRingVol) { }
    }

    public class MicrophoneVolumeViewModel : VolumeViewModel
    {
        public MicrophoneVolumeViewModel() : base(NativeMethods.Dll_GetVolImport, NativeMethods.Dll_SetVolImport) { }
    }

    public abstract class VolumeViewModel : ViewModelBase
    {
        protected readonly Func<int> _getVolume;
        protected readonly Func<IntPtr, int> _setVolume;

        public VolumeViewModel(
            Func<int> getVolume,
            Func<IntPtr, int> setVolume)
        {
            _getVolume = getVolume;
            _setVolume = setVolume;
        }

        public double MaxValue
        {
            get { return 100; }
        }

        public double MinValue
        {
            get { return 0; }
        }

        double _CurrentValue;
        public double CurrentValue
        {
            get
            {
                _CurrentValue = _getVolume();
                return _CurrentValue;
            }
            set
            {
                if (MinValue <= value && value <= MaxValue)
                {
                    if (this.Set(ref _CurrentValue, value))
                    {
                        int intValue = (int)value;
                        _setVolume((IntPtr)intValue);
                    }
                }
            }
        }
    }
}
