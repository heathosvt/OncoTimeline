using System;
using System.Collections.Generic;
using OncoTimeline.Domain.Entities;

namespace OncoTimeline.Infrastructure.Data;

public static class DrugSeeder
{
    public static List<Drug> GetSeedDrugs()
    {
        var vincristineId = Guid.NewGuid();
        var methotrexateId = Guid.NewGuid();
        var daunorubicinId = Guid.NewGuid();

        return new List<Drug>
        {
            new Drug
            {
                Id = vincristineId,
                Name = "Vincristine",
                DrugClass = "Vinca Alkaloid",
                MechanismOfAction = "Inhibits microtubule formation, preventing cancer cells from dividing",
                WhyUsedInLeukemia = "Stops leukemia cells from multiplying by disrupting their ability to divide",
                ParentFriendlyExplanation = "Vincristine works like a stop sign for cancer cells. It prevents them from splitting into more cancer cells, which helps control the leukemia. Think of it as freezing the cancer cells so they can't grow.",
                TypicalOnsetTiming = "Effects begin within hours; side effects may appear within 1-7 days",
                DurationOfEffects = "Remains active for several days; side effects may persist 1-2 weeks",
                ExpectedLabChanges = "May indirectly affect blood counts as bone marrow recovers from leukemia. Vincristine itself doesn't typically cause severe bone marrow suppression, but combined therapy does.",
                NeurologicalImpacts = "Can cause peripheral neuropathy (tingling, numbness in fingers/toes), constipation, jaw pain",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                SideEffects = new List<DrugSideEffect>
                {
                    new DrugSideEffect
                    {
                        Id = Guid.NewGuid(),
                        DrugId = vincristineId,
                        EffectName = "Peripheral Neuropathy",
                        Severity = "Common",
                        Description = "Tingling, numbness, or pain in hands and feet",
                        TypicalOnset = "1-2 weeks after treatment"
                    },
                    new DrugSideEffect
                    {
                        Id = Guid.NewGuid(),
                        DrugId = vincristineId,
                        EffectName = "Constipation",
                        Severity = "Common",
                        Description = "Difficulty with bowel movements due to nerve effects on intestines",
                        TypicalOnset = "Within days of treatment"
                    },
                    new DrugSideEffect
                    {
                        Id = Guid.NewGuid(),
                        DrugId = vincristineId,
                        EffectName = "Jaw Pain",
                        Severity = "Common",
                        Description = "Temporary jaw discomfort",
                        TypicalOnset = "1-3 days after infusion"
                    }
                }
            },
            new Drug
            {
                Id = methotrexateId,
                Name = "Methotrexate",
                DrugClass = "Antimetabolite",
                MechanismOfAction = "Blocks folic acid metabolism, preventing DNA synthesis in rapidly dividing cells",
                WhyUsedInLeukemia = "Targets fast-growing leukemia cells by stopping them from making DNA",
                ParentFriendlyExplanation = "Methotrexate blocks a vitamin (folic acid) that cancer cells need to grow. Without it, the cancer cells can't build new DNA and eventually die. Normal cells are affected too, which is why we give leucovorin (a rescue medication) to protect healthy cells.",
                TypicalOnsetTiming = "Works within hours; side effects appear 3-7 days later",
                DurationOfEffects = "Cleared from body in 24-48 hours with normal kidney function",
                ExpectedLabChanges = "Can cause drops in WBC, platelets, and hemoglobin 7-14 days after administration (bone marrow suppression)",
                NeurologicalImpacts = "High-dose or intrathecal methotrexate can rarely cause confusion, seizures, or leukoencephalopathy",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                SideEffects = new List<DrugSideEffect>
                {
                    new DrugSideEffect
                    {
                        Id = Guid.NewGuid(),
                        DrugId = methotrexateId,
                        EffectName = "Mucositis",
                        Severity = "Common",
                        Description = "Mouth sores and inflammation",
                        TypicalOnset = "3-7 days after treatment"
                    },
                    new DrugSideEffect
                    {
                        Id = Guid.NewGuid(),
                        DrugId = methotrexateId,
                        EffectName = "Bone Marrow Suppression",
                        Severity = "Serious",
                        Description = "Low blood counts (WBC, platelets, hemoglobin)",
                        TypicalOnset = "7-14 days after treatment"
                    },
                    new DrugSideEffect
                    {
                        Id = Guid.NewGuid(),
                        DrugId = methotrexateId,
                        EffectName = "Liver Toxicity",
                        Severity = "Serious",
                        Description = "Elevated liver enzymes; monitored via blood tests",
                        TypicalOnset = "During or shortly after treatment"
                    }
                }
            },
            new Drug
            {
                Id = daunorubicinId,
                Name = "Daunorubicin",
                DrugClass = "Anthracycline",
                MechanismOfAction = "Intercalates DNA and inhibits topoisomerase II, causing DNA damage in cancer cells",
                WhyUsedInLeukemia = "Directly damages the DNA of leukemia cells, causing them to die",
                ParentFriendlyExplanation = "Daunorubicin is a powerful chemotherapy drug that gets inside cancer cells and breaks their DNA. Once the DNA is damaged, the cancer cells can't survive. It's one of the strongest drugs used during induction therapy to quickly reduce leukemia cells.",
                TypicalOnsetTiming = "Immediate effect on cancer cells; side effects appear within days",
                DurationOfEffects = "Cleared from body in 24-48 hours; effects on heart are cumulative over lifetime",
                ExpectedLabChanges = "Significant bone marrow suppression: expect low WBC, ANC, platelets, and hemoglobin 10-14 days after treatment",
                NeurologicalImpacts = "Minimal direct neurological effects",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                SideEffects = new List<DrugSideEffect>
                {
                    new DrugSideEffect
                    {
                        Id = Guid.NewGuid(),
                        DrugId = daunorubicinId,
                        EffectName = "Bone Marrow Suppression",
                        Severity = "Serious",
                        Description = "Severe drop in all blood counts",
                        TypicalOnset = "10-14 days after treatment"
                    },
                    new DrugSideEffect
                    {
                        Id = Guid.NewGuid(),
                        DrugId = daunorubicinId,
                        EffectName = "Cardiotoxicity",
                        Severity = "Serious",
                        Description = "Can weaken heart muscle with cumulative doses; monitored via echocardiograms",
                        TypicalOnset = "Cumulative over multiple doses"
                    },
                    new DrugSideEffect
                    {
                        Id = Guid.NewGuid(),
                        DrugId = daunorubicinId,
                        EffectName = "Red/Orange Urine",
                        Severity = "Common",
                        Description = "Harmless discoloration of urine for 1-2 days",
                        TypicalOnset = "Within 24 hours"
                    },
                    new DrugSideEffect
                    {
                        Id = Guid.NewGuid(),
                        DrugId = daunorubicinId,
                        EffectName = "Nausea and Vomiting",
                        Severity = "Common",
                        Description = "Can be severe; managed with anti-nausea medications",
                        TypicalOnset = "Within hours to days"
                    }
                }
            }
        };
    }
}
