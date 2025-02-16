namespace TurnoverMeBackend.Application.DTO;

public class CreateWorkflowRequest
{
    public string name { get; set; }
    public StageDto[] stages { get; set; }

    public class StageDto
    {
        public string GroupId { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
