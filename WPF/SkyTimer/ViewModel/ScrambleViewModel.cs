using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using SkyTimer.Model;
using SkyTimer.Service;

namespace SkyTimer.ViewModel
{
    public class ScrambleViewModel : ViewModelBase
    {
        public ScrambleViewModel(IScrambleService scramble)
        {
            scrambleService = scramble;
            Messenger.Default.Register<UpdateScrambleInstruction>(this, UpdateScramble);
        }

        private IScrambleService scrambleService;

        private string scramble;
        public string Scramble
        {
            get { return scramble; }
            set { Set(ref scramble, value); }
        }

        public async void UpdateScramble(UpdateScrambleInstruction ins)
        {
            Scramble = await scrambleService.GetScramble(ins.CubeName);
            Messenger.Default.Send(Scramble);
        }
    }
}
