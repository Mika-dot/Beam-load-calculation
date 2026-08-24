using EngineeringCalculator.Common;

namespace EngineeringCalculator.Sections;

public static class SectionFactory
{
    public static Section Rectangle(string name, double width, double height, double density = 7850)
    {
        Guard.Positive(width, nameof(width)); Guard.Positive(height, nameof(height));
        var area = width * height;
        var inertia = width * Math.Pow(height, 3) / 12;
        return Create(name, "Прямоугольник", area, inertia, inertia / (height / 2), height, density);
    }

    public static Section Circle(string name, double diameter, double density = 7850)
    {
        Guard.Positive(diameter, nameof(diameter));
        var area = Math.PI * diameter * diameter / 4;
        var inertia = Math.PI * Math.Pow(diameter, 4) / 64;
        return Create(name, "Круг", area, inertia, inertia / (diameter / 2), diameter, density);
    }

    public static Section RectangularHollow(string name, double width, double height, double thickness, double density = 7850)
    {
        Guard.Positive(width, nameof(width)); Guard.Positive(height, nameof(height)); Guard.Positive(thickness, nameof(thickness));
        if (2 * thickness >= Math.Min(width, height)) throw new ArgumentException("Толщина стенки слишком велика.", nameof(thickness));
        var innerWidth = width - 2 * thickness;
        var innerHeight = height - 2 * thickness;
        var area = width * height - innerWidth * innerHeight;
        var inertia = (width * Math.Pow(height, 3) - innerWidth * Math.Pow(innerHeight, 3)) / 12;
        return Create(name, "Прямоугольная труба", area, inertia, inertia / (height / 2), height, density);
    }

    public static Section CircularHollow(string name, double diameter, double thickness, double density = 7850)
    {
        Guard.Positive(diameter, nameof(diameter)); Guard.Positive(thickness, nameof(thickness));
        if (2 * thickness >= diameter) throw new ArgumentException("Толщина стенки слишком велика.", nameof(thickness));
        var inner = diameter - 2 * thickness;
        var area = Math.PI * (diameter * diameter - inner * inner) / 4;
        var inertia = Math.PI * (Math.Pow(diameter, 4) - Math.Pow(inner, 4)) / 64;
        return Create(name, "Круглая труба", area, inertia, inertia / (diameter / 2), diameter, density);
    }

    public static Section ISection(string name, double height, double flangeWidth, double webThickness, double flangeThickness, double density = 7850)
    {
        Guard.Positive(height, nameof(height)); Guard.Positive(flangeWidth, nameof(flangeWidth));
        Guard.Positive(webThickness, nameof(webThickness)); Guard.Positive(flangeThickness, nameof(flangeThickness));
        if (2 * flangeThickness >= height || webThickness >= flangeWidth) throw new ArgumentException("Некорректная геометрия двутавра.");
        var webHeight = height - 2 * flangeThickness;
        var area = 2 * flangeWidth * flangeThickness + webThickness * webHeight;
        var inertia = 2 * (flangeWidth * Math.Pow(flangeThickness, 3) / 12 + flangeWidth * flangeThickness * Math.Pow((height - flangeThickness) / 2, 2))
                      + webThickness * Math.Pow(webHeight, 3) / 12;
        return Create(name, "Двутавр", area, inertia, inertia / (height / 2), height, density);
    }

    private static Section Create(string name, string family, double area, double inertia, double modulus, double height, double density) => new()
    {
        Id = name.Trim().ToLowerInvariant().Replace(' ', '-'), Name = name, Family = family,
        Area = area, MomentOfInertia = inertia, SectionModulus = modulus, Height = height, MassPerMetre = area * density
    };
}
