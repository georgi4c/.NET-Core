namespace SocialNetwork.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SocialNetwork.Models;

    public class UserFriendConfiguration : IEntityTypeConfiguration<UserFriend>
    {
        public void Configure(EntityTypeBuilder<UserFriend> builder)
        {
            builder
                .ToTable("UserFriends")
                .HasKey(uf => new { uf.UserId, uf.FriendId });

            builder
                .HasOne(uf => uf.User)
                .WithMany(u => u.Friends)
                .HasForeignKey(uf => uf.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(uf => uf.Friend)
                .WithMany(f => f.FriendTo)
                .HasForeignKey(uf => uf.FriendId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
