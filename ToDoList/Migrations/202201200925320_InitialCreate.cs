namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToDoItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        IsDone = c.Boolean(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        AssignedTo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.AssignedTo, cascadeDelete: true)
                .Index(t => t.AssignedTo);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDoItem", "AssignedTo", "dbo.User");
            DropIndex("dbo.ToDoItem", new[] { "AssignedTo" });
            DropTable("dbo.User");
            DropTable("dbo.ToDoItem");
        }
    }
}
