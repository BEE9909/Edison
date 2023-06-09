using r_framework.Entity;

namespace Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.Entity
{
    /// <summary>
    /// Nxîñf[^NX
    /// </summary>
    public class NendoInfo : SuperEntity
    {
        // SQLæèæ¾·éÚ
        public string BUSHO_CD { get; set; }    //R[h
        public string BUSHO_NAME { get; set; }  //¼
        public string SHAIN_CD { get; set; }    //ÐõR[h
        public string SHAIN_NAME { get; set; }  //Ðõ¼
        public decimal YOSAN_1 { get; set; }    //N1Ì\Z
        public decimal YOSAN_2 { get; set; }    //N2Ì\Z
        public decimal YOSAN_3 { get; set; }    //N3Ì\Z
        public decimal YOSAN_4 { get; set; }    //N4Ì\Z
        public decimal YOSAN_5 { get; set; }    //N5Ì\Z
        public decimal YOSAN_6 { get; set; }    //N6Ì\Z
        public decimal YOSAN_7 { get; set; }    //N7Ì\Z
        public decimal YOSAN_8 { get; set; }    //N8Ì\Z
        public decimal YOSAN_9 { get; set; }    //N9Ì\Z
        public decimal YOSAN_GOUKEI { get; set; }     //\Zv
        public decimal JISSEKI_1 { get; set; }  //N1ÌÀÑ
        public decimal JISSEKI_2 { get; set; }  //N2ÌÀÑ
        public decimal JISSEKI_3 { get; set; }  //N3ÌÀÑ
        public decimal JISSEKI_4 { get; set; }  //N4ÌÀÑ
        public decimal JISSEKI_5 { get; set; }  //N5ÌÀÑ
        public decimal JISSEKI_6 { get; set; }  //N6ÌÀÑ
        public decimal JISSEKI_7 { get; set; }  //N7ÌÀÑ
        public decimal JISSEKI_8 { get; set; }  //N8ÌÀÑ
        public decimal JISSEKI_9 { get; set; }  //N9ÌÀÑ
        public decimal JISSEKI_GOUKEI { get; set; }   //ÀÑv

        // ÒWÊÚ
        public decimal TASSEI_RITSU_1 { get; set; }  //N1ÌB¬¦
        public decimal TASSEI_RITSU_2 { get; set; }  //N2ÌB¬¦
        public decimal TASSEI_RITSU_3 { get; set; }  //N3ÌB¬¦
        public decimal TASSEI_RITSU_4 { get; set; }  //N4ÌB¬¦
        public decimal TASSEI_RITSU_5 { get; set; }  //N5ÌB¬¦
        public decimal TASSEI_RITSU_6 { get; set; }  //N6ÌB¬¦
        public decimal TASSEI_RITSU_7 { get; set; }  //N7ÌB¬¦
        public decimal TASSEI_RITSU_8 { get; set; }  //N8ÌB¬¦
        public decimal TASSEI_RITSU_9 { get; set; }  //N9ÌB¬¦
        public decimal TASSEI_GOKEI { get; set; }   //B¬¦v
    }
}