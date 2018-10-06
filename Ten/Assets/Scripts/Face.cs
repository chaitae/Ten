using System.IO;
using UnityEngine;
namespace GhostFaceGenerator
{
    public class GhostFace
    {
        //Statics
        static private string[] leftEyes = Directory.GetFiles(@"GhostFacePieces\LeftEyes");
        static private string[] rightEyes = Directory.GetFiles(@"GhostFacePieces\RightEyes");
        static private string[] mouths = Directory.GetFiles(@"GhostFacePieces\Mouths");

        static public int GetLeftEyesCount() { return leftEyes.Length; }
        static public int GetRightEyesCount() { return rightEyes.Length; }
        static public int GetMouthsCount() { return mouths.Length; }


        //Instance constructors
        public GhostFace() : this(0, 0, 0) { }

        public GhostFace(int lEye, int rEye, int mouth)
        {
            LeftEye = leftEyes[lEye];
            RightEye = rightEyes[rEye];
            Mouth = mouths[mouth];
        }

        //Props
        [SerializeField]
        public string LeftEye { get; set; }
        public string RightEye { get; set; }
        public string Mouth { get; set; }
    }
}
