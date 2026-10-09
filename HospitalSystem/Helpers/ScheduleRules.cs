namespace HospitalSystem.Helpers;

public static class ScheduleRules
{
    public static readonly TimeSpan FirstSlot = new(9, 0, 0);
    public static readonly TimeSpan LastSlot = new(16, 30, 0);
    public const int SlotMinutes = 30;

    /// <summary>9:00 AM → 4:30 PM every 30 minutes.</summary>
    public static IReadOnlyList<TimeSpan> TimeSlots { get; } = BuildSlots();

    /// <summary>Clinic works Sunday → Thursday.</summary>
    public static bool IsWorkingDay(DateTime date) =>
        date.DayOfWeek is not (DayOfWeek.Friday or DayOfWeek.Saturday);

    public static bool IsValidSlot(TimeSpan time) => TimeSlots.Contains(time);

    public static string Format(TimeSpan time) => DateTime.Today.Add(time).ToString("h:mm tt");

    private static List<TimeSpan> BuildSlots()
    {
        var slots = new List<TimeSpan>();
        for (var t = FirstSlot; t <= LastSlot; t = t.Add(TimeSpan.FromMinutes(SlotMinutes)))
            slots.Add(t);
        return slots;
    }
}
