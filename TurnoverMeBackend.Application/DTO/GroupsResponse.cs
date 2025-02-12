namespace TurnoverMeBackend.Application.DTO;

public class GroupsResponse
{
    public List<GroupDto> Groups { get; set; }

    public class GroupDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<UserDto> Users { get; set; }

        public class UserDto
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }
}