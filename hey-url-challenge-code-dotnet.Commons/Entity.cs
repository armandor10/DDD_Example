using System;
namespace hey_url_challenge_code_dotnet.Commons
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedOn { get; protected set; }
        public DateTime UpdateOn { get; protected set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
            UpdateOn = DateTime.Now;
        }
    }
}

