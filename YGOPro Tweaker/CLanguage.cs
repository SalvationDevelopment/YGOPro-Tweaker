using System;
using System.Collections.Generic;
using System.Text;


namespace YGOPro_Tweaker
{
    class CLanguage
    {
        public class Main
        {
            public class Thai
            {
                public String Title { get { return String.Format("เมนูหลัก - YGOPro Tweaker {0}",  System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion); } }
                public String Config { get { return "ตั้งค่าเกม YGOPro"; } }
                public String Deck_List { get { return "สร้างเดคลิสจากไฟล์เดค (.ydk > decklist)"; } }
                public String Deck_Extractor { get { return "สร้างไฟล์เดคจากรีเพล์ (.yrp > .ydk)"; } }
                public String Credit { get { return String.Format("YGOPro Tweaker {0}\nพัฒนาโดย ekaomk", System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion); } }
                public String Agreement_Text { get { return "ข้อตกลง"; } }
                public String Deck_Extractor_Agreement_Text { get { return "โปรแกรมสร้างไฟล์เดคจากรีเพล์ หรือ Deck Extractor จัดทำขึ้นมาเพื่อศึกษาระบบไฟล์รีเพล์ของ YGOPro ไม่ได้มีจุดประสงค์อื่นแต่อย่างใด หากผู้ใช้งานนำไปใช้งานในทางที่ผิด หรือทำให้เกิดความเดือดร้อน หรือเกิดความเสียหายต่อตนเองและผู้อื่น ทางเราจะไม่รับผิดชอบใดๆทั้งสิ้น\nหากกดตกลงถือว่าคุณได้อ่าน และยอมรับเงื่อนไขนี้แล้ว"; } }
            }

            public class English
            {
                public String Title { get { return String.Format("Main menu - YGOPro Tweaker {0}", System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion); } }
                public String Agreement_Text { get { return "Agreement"; } }
                public String Deck_Extractor_Agreement_Text { get { return "This software (Deck Extractor) is develop for learn about replay file of YGOPro. Did not have any other purpose, If the user has used in the wrong way. Or cause suffering or damage to themselves and others. We are not responsible in any way.\nIf you press Yes, assume you have read. And accept this"; } }
                public String Credit { get { return String.Format("YGOPro Tweaker {0}\nDevelop by ekaomk", System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion); } }
            }
        }

        public class DeckExtractor
        {
            public class Thai
            {
                public String Title { get { return String.Format("สร้างไฟล์เดคจากรีเพล์ - YGOPro Tweaker {0}", System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion); } }
                ///////////////////////////////////////////////////////////////////
                public String Load_Replay_Text { get { return "โหลดรีเพล์"; } }
                public String Mode_Text { get { return "โหมด : {0}"; } }
                public String Player_Text { get { return "ผู้เล่น : {0}"; } }

                public String Start_Life_Point_Text { get { return "ไลฟ์พ้อยเริ่มต้น : {0}"; } }
                public String Start_Hand_Text { get { return "จำนวนการ์ดเริ่มต้น : {0}"; } }
                public String Draw_For_Text { get { return "ดรอว์ครั้งละ : {0}"; } }

                public String Main_Deck_Title { get { return "============ เมนเดค ============"; } }
                public String Extra_Deck_Title { get { return "============ เอ็กทราเดค ============"; } }
                public String Side_Deck_Title { get { return "============ ไซด์เดค ============"; } }

                public String Deck_Owner_Text { get { return "เดคของ {0}"; } }
                public String Copy_Deck_List_Text { get { return "คัดลอกเดคลิสของ {0}"; } }
                public String Save_Deck_List_Text { get { return "บันทึกเดคลิสของ {0}"; } }

                public String Mode_Single_or_Match_Text { get { return "โหมด : ซิงเกิ้ลหรือแมทซ์"; } }
                public String Mode_1vs2_Text { get { return "โหมด : แบทเทิ้ลรอยัล"; } }
                public String Mode_Tag_Text { get { return "โหมด : แท็ก"; } }
                public String Player1_Text { get { return "ผู้เล่น 1 : {0}{1}"; } }
                public String Player2_Text { get { return "ผู้เล่น 2 : {0}{1}"; } }
                public String Player3_Text { get { return "ผู้เล่น 3 : {0}{1}"; } }
                public String Player4_Text { get { return "ผู้เล่น 4 : {0}{1}"; } }
                public String VS_Text { get { return "VS{0}"; } }
                public String Copied_To_Clipboard_Text { get { return "คัดลอกลงคลิปบอร์ดเรียบร้อยแล้ว"; } }
                public String Information_Text { get { return "ข้อมูล"; } }
                public String Error_Text { get { return "เกิดข้อผิดพลาด"; } }
                public String Replay_Error_Text { get { return "รีเพล์ผิดพลาดหรือเป็นเวอร์ชันเก่า. โปรแกรมหาไอดีการ์ดนี้ไม่พบ.{0}ไอดีการ์ด : {1}"; } }
                public String Ignore_Card_Missing_Text { get { return "ไม่สนใจหากการ์ดหาย ( สำหรับ Expansion )"; } }
            }

            public class English
            {
                public String Title { get { return String.Format("Deck Extractor - YGOPro Tweaker {0}", System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion); } }
                public String Load_Replay_Text { get { return "Load Replay"; } }
                public String Mode_Text { get { return "Mode : {0}"; } }
                public String Player_Text { get { return "Player : {0}"; } }
                public String Start_Life_Point_Text { get { return "Start Life Point : {0}"; } }
                public String Start_Hand_Text { get { return "Start Hand : {0}"; } }
                public String Draw_For_Text { get { return "Draw For : {0}"; } }
                public String Main_Deck_Title { get { return "============ Main Deck ============"; } }
                public String Extra_Deck_Title { get { return "============ Extra Deck ============"; } }
                public String Side_Deck_Title { get { return "============ Side Deck ============"; } }
                public String Deck_Owner_Text { get { return "{0} Deck"; } }
                public String Copy_Deck_List_Text { get { return "Copy {0} Deck List"; } }
                public String Save_Deck_List_Text { get { return "Save {0} Deck List"; } }
                public String Mode_Single_or_Match_Text { get { return "Mode : Single or Match"; } }
                public String Mode_1vs2_Text { get { return "Mode : 1vs2"; } }
                public String Mode_Tag_Text { get { return "Mode : Tag"; } }
                public String Player1_Text { get { return "Player 1 : {0}{1}"; } }
                public String Player2_Text { get { return "Player 2 : {0}{1}"; } }
                public String Player3_Text { get { return "Player 3 : {0}{1}"; } }
                public String Player4_Text { get { return "Player 4 : {0}{1}"; } }
                public String VS_Text { get { return "VS{0}"; } }
                public String Copied_To_Clipboard_Text { get { return "Copied To Clipboard."; } }
                public String Information_Text { get { return "Information"; } }
                public String Error_Text { get { return "Error"; } }
                public String Replay_Error_Text { get { return "Replay is invalid or old version. Program can not find card from ID.{0}Card ID : {1}"; } }
            }
        }

        public class DeckList
        {
            public class Thai
            {
                public String Title { get { return String.Format("สร้างเดคลิสจากไฟล์เดค - YGOPro Tweaker {0}", System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion); } }
                ///////////////////////////////////////////////////////////////////
                public String Main_Deck_Title { get { return "============ เมนเดค : {0} ============"; } }
                public String Extra_Deck_Title { get { return "============ เอ็กทราเดค : {0} ============"; } }
                public String Side_Deck_Title { get { return "============ ไซด์เดค : {0} ============"; } }
                public String Deck_Error_Text { get { return "เดคผิดพลาดหรือเป็นเวอร์ชันเก่า. โปรแกรมหาไอดีการ์ดนี้ไม่พบ.{0}ไอดีการ์ด : {1}"; } }
                public String Error_Text { get { return "เกิดข้อผิดพลาด"; } }
                public String Load_Deck_Text { get { return "โหลดไฟล์เดค"; } }
                public String Copy_To_Clipboard_Text { get { return "คัดลอกลงคลิปบอร์ด"; } }
                public String Copied_To_Clipboard_Text { get { return "คัดลอกลงคลิปบอร์ดเรียบร้อยแล้ว"; } }
                public String Information_Text { get { return "ข้อมูล"; } }

            }

            public class English
            {
                public String Title { get { return String.Format("Deck List Builder - YGOPro Tweaker {0}", System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion); } }
                public String Main_Deck_Title { get { return "============ Main Deck : {0} ============"; } }
                public String Extra_Deck_Title { get { return "============ Extra Deck : {0} ============"; } }
                public String Side_Deck_Title { get { return "============ Side Deck : {0} ============"; } }
                public String Deck_Error_Text { get { return "Deck is invalid or old version. Program can not find card from ID.{0}Card ID : {1}"; } }

                public String Error_Text { get { return "Error"; } }
                public String Load_Deck_Text { get { return "Load Deck"; } }

                public String Copy_To_Clipboard_Text { get { return "Copy To Clipboard"; } }
                public String Copied_To_Clipboard_Text { get { return "Copied To Clipboard."; } }
                public String Information_Text { get { return "Infomation"; } }
            }
        }

        public class Config
        {
            public class Thai
            {
                public String Title { get { return String.Format("การตั้งค่าเกม - YGOPro Tweaker {0}", System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion); } }
                public String Language { get { return "ภาษา"; } }
                public String English { get { return "อังกฤษ"; } }
                public String German { get { return "เยอรมัน"; } }
                public String Spanish { get { return "สเปน"; } }
                public String _Thai { get { return "ไทย"; } }
                public String Direct3D { get { return "การแสดงผลด้วย Direct3D"; } }
                public String Enable { get { return "เปิด"; } }
                public String Disable { get { return "ปิด"; } }
                public String Skin { get { return "การใช้งานสกิน"; } }
                public String Anti_aliasing { get { return "ลบรอยหยัก"; } }
                public String Error_Log { get { return "บันทึกข้อผิดพลาด"; } }
                public String Nickname { get { return "ชื่อเล่นในเกมส์"; } }
                public String Text_Font { get { return "ฟ้อนตัวอักษร"; } }
                public String Font_Path { get { return "ตำแหน่ง"; } }
                public String Font_Size { get { return "ขนาด"; } }
                public String Number_Font { get { return "ฟ้อนตัวเลข"; } }
                public String Fullscreen { get { return "โหมดหน้าจอ"; } }
                public String FullscreenMode { get { return "เต็มหน้าจอ"; } }
                public String WindowedMode { get { return "หน้าต่าง"; } }
                public String Sound { get { return "เสียงเอฟเฟค"; } }
                public String Music { get { return "เสียงเพลง"; } }
                public String Auto_Card_Placing { get { return "วางการ์ดอัตโนมัติ"; } }
                public String Random_Card_Placing { get { return "วางการ์ดแบบสุ่ม"; } }
                public String Auto_Chain_Order { get { return "เรียงเชนอัตโนมัติ"; } }
                public String No_Delay_For_Chain { get { return "เชนไม่มีดีเลย์"; } }
                public String Mute_Opponent { get { return "ปิดการแชทของอีกฝ่าย"; } }
                public String Mute_Spectators { get { return "ปิดการแชทของผู้ชม"; } }
                public String Volume { get { return "ระดับเสียง"; } }
                public String Background { get { return "พื้นหลัง"; } }
                public String Duel_Zone_Only { get { return "พื้นที่ดูเอลอย่างเดียว"; } }
                public String Background_Only { get { return "พื้นหลังอย่างเดียว"; } }
                public String Both_Duel_Zone_And_Background { get { return "ทั้งพื้นที่ดูเอลและพื้นหลัง"; } }
                public String Do_Not_Change_Background { get { return "ไม่เปลื่ยนพื้นหลัง"; } }
                public String Save { get { return "บันทึก"; } }
                public String Reset { get { return "รีเซ็ต"; } }
                public String Load_Default_Config { get { return "โหลดค่ามาตรฐาน"; } }
            }

            public class English
            {
                public String Title { get { return String.Format("YGOPro Config - YGOPro Tweaker {0}", System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion); } }
               
            }
        }
    }
}
