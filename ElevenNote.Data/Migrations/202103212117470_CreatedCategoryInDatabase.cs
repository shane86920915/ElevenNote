namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedCategoryInDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        CategoryName = c.String(),
                        NoteId = c.Int(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Note", t => t.NoteId, cascadeDelete: true)
                .Index(t => t.NoteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Category", "NoteId", "dbo.Note");
            DropIndex("dbo.Category", new[] { "NoteId" });
            DropTable("dbo.Category");
        }
    }
}
