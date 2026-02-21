using System;
using System.Collections.Generic;

namespace OncoTimeline.Application.DTOs;

public record TimelineEventDto(
    Guid Id,
    Guid PatientId,
    Guid? TreatmentPhaseId,
    string Title,
    DateTime EventDate,
    string Category,
    string Notes,
    string Tags,
    List<DrugDto> Drugs
);

public record DrugDto(
    Guid Id,
    string Name,
    string DrugClass,
    string ParentFriendlyExplanation,
    string Dosage,
    string Route
);

public record CreateTimelineEventDto(
    Guid PatientId,
    Guid? TreatmentPhaseId,
    string Title,
    DateTime EventDate,
    string Category,
    string Notes,
    string Tags,
    List<EventDrugDto> Drugs
);

public record EventDrugDto(
    Guid DrugId,
    string Dosage,
    string Route
);

public record UpdateTimelineEventDto(
    string Title,
    DateTime EventDate,
    string Category,
    string Notes,
    string Tags,
    List<EventDrugDto> Drugs
);
