using System;

namespace OncoTimeline.Application.DTOs;

public record TreatmentPhaseDto(
    Guid Id,
    Guid PatientId,
    string Name,
    string Description,
    DateTime StartDate,
    DateTime? EndDate,
    int DisplayOrder,
    string Color
);

public record CreateTreatmentPhaseDto(
    Guid PatientId,
    string Name,
    string Description,
    DateTime StartDate,
    DateTime? EndDate,
    int DisplayOrder,
    string Color
);
