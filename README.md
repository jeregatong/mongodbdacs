mongodbdacs
===========

MongoDB Data Access (C#)

// -------------------------------------------App.config.xml------------------------------------------

    <appSettings>
      <add key ="db" value ="schooldb" />
    </appSettings>
    <connectionStrings>
      <add name ="conStr" connectionString ="mongodb://teacher1:1234@localhost:27017"/>
    </connectionStrings>

// -------------------------------------------Student.cs----------------------------------------------

    [BsonDiscriminator("student")]
    public class Student
    {
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }
    }

// ----------------------------------------------CRUD-------------------------------------------------
    
    // Create
    DataManipulation<Student>.Add(new Student { Id = ObjectId.GenerateNewId(), Name = "Jeremiah Gatong", Address = "Taguig" });
    DataManipulation<Student>.Add(new Student { Id = ObjectId.GenerateNewId(), Name = "Cathy Sebalda", Address = "Taguig" });

    // Read All
    var students = DataManipulation<Student>.Get();

    var result = from student in students
                    select student;

    foreach (var s in result)
    {
        MessageBox.Show(s.Name);
    }

    // Read Single
    MessageBox.Show(DataManipulation<Student>.Get(new ObjectId("50ef682e05d96c2458dfdbdc")).Name);

    // Update
    DataManipulation<Student>.Edit(new ObjectId("50ef682e05d96c2458dfdbdc"), new Student { Name = "Miah", Address = "Taguig" });

    // Delete All
    DataManipulation<Student>.Erase();

    // Delete Single
    DataManipulation<Student>.Erase(new ObjectId("50ef69ac05d96c14883b6711"));
