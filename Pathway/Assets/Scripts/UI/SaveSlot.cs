[System.Serializable]
public class SaveSlot
{
    public string[] name= new string[16];
    public SaveSlot(SlotManager sm)
    {
        for(int i=0;i<16;i++)
        {
            name[i]=sm.ss[i];
        }
    }


}

