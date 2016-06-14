using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using SkyTimer.Model;
using SkyTimer.Utils.Scramble;

namespace SkyTimer.ViewModel
{
    public class ScrambleViewModel : ViewModelBase
    {
        public ScrambleViewModel(IScrambleService scrambler)
        {
            this.scrambler = scrambler;

            Messenger.Default.Register<UpdateScrambleInstruction>(this, UpdateScramble);

        }

        private IScrambleService scrambler;

        private string scramble;
        public string Scramble
        {
            get { return scramble; }
            set { Set(ref scramble, value); }
        }

        public void UpdateScramble(UpdateScrambleInstruction ins)
        {
            Scramble = scrambler.GetScramble(ins.CubeName);

            Messenger.Default.Send(Scramble);
        }
    }
}
