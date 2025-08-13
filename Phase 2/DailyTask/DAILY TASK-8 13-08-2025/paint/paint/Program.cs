using paint;

IBrush brush = Company.Fetchbrush(false);

Painter painter = new Painter( "Sanjeev" , brush );

Console.WriteLine( $"Name is {painter.Name}. He uses {brush.Proc} size of {brush.Size} using the brand {brush.Variant}" );