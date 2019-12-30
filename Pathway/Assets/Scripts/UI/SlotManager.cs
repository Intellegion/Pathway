using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class SlotManager : MonoBehaviour
{
    [SerializeField] private Button[] slots = new Button[16];
    public static int slot_no;
    public static bool save_load;
    [HideInInspector] public string[] ss = new string[16];

    // Start is called before the first frame update
    void Start()
    {
        slot_no = 1;
        SaveSlot curr_game = SlotSave.Instance.Loadgame();
        save_load = false;
        if(curr_game!=null)
        {
            for(int i = 0;i<16;i++)
            {
                ss[i] = curr_game.name[i];
                slots[i].GetComponentInChildren<TextMeshProUGUI>().text=curr_game.name[i];
            }
        }
    }
    public void Click(Button uwu)
    {
       slot_no = Int32.Parse(uwu.name);
    }
    public void Save_Load(TMP_InputField sn)
    {
        LevelEditor le = GetComponent<LevelEditor>();
        if(save_load)
        {
            if(sn.text!=null)
            {
                le.SaveGame(sn.text);
                ss[slot_no-1]=sn.text;
                slots[slot_no-1].GetComponentInChildren<TextMeshProUGUI>().text=sn.text;
                SlotSave.Instance.Savegame(this);
            }
        }
        else
        {
            sn.enabled=false;
            if(ss.Length<slot_no-1)
                le.LoadGame(ss[slot_no-1]);
        }
        sn.text = null;
        le.Show();
        
    }
}
