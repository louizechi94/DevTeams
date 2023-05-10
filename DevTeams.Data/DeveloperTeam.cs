
public class DeveloperTeam

{

    public DeveloperTeam()
    {
        
    }
    public DeveloperTeam(string teamName)
    {
      TeamName = teamName;  
    }
    public DeveloperTeam(string teamName, List<Developer> developer)
    {
      TeamName = teamName;
      Developers = Developers;  
    }
    public int ID { get; set; }
    public string TeamName { get; set; }= string.Empty;
    public List<Developer> Developers  { get; set; }= new List<Developer>();

  public override string ToString()
  {
    var str = $"ID: {ID}\n"+
    $"Team Name: {TeamName}\n"+
    "=== Team Memebers ===\n";
    foreach(var dev in Developers)
    {
      str+=$"{dev}\n";
    }
    return str;
  } 
    
   
}