using System;
using System.Collections.Generic;
using OncoTimeline.Domain.Entities;

namespace OncoTimeline.Infrastructure.Data;

public static class DrugSeeder
{
    public static List<Drug> GetSeedDrugs()
    {
        var drugs = new List<Drug>();

        // 1. Vincristine
        var vincristineId = Guid.NewGuid();
        drugs.Add(new Drug
        {
            Id = vincristineId,
            Name = "Vincristine",
            DrugClass = "Vinca Alkaloid (IV push - given through central line)",
            MechanismOfAction = "Inhibits microtubule formation during cell division, causing mitotic arrest",
            WhyUsedInLeukemia = "Stops leukemia cells from dividing by disrupting their internal structure",
            ParentFriendlyExplanation = "Vincristine freezes cancer cells so they can't split and multiply. It's like putting a stop sign inside each cancer cell. Given weekly during induction.",
            TypicalOnsetTiming = "Works immediately; side effects appear within 1-7 days",
            DurationOfEffects = "Active for several days; nerve effects may last weeks",
            ExpectedLabChanges = "Minimal direct effect on blood counts (unlike other chemo drugs)",
            NeurologicalImpacts = "Peripheral neuropathy (tingling/numbness in hands/feet), constipation, jaw pain, vocal cord weakness, autonomic neuropathy affecting swallowing coordination and voice",
            TypicalTimeline = @"Day 0: IV push (takes seconds)
Day 1-3: Jaw pain may appear (temporary)
Day 3-7: Constipation begins (give stool softeners)
Day 7-14: Nerve effects peak (tingling, voice changes)
Day 14-21: Gradual improvement
Note: Effects are cumulative with repeated doses",
            MonitoringReason = @"• Nerve toxicity (peripheral and autonomic)
• Vocal cord weakness (can affect speech/swallowing)
• Severe constipation risk
• Cumulative neuropathy with repeated doses",
            CommonButNotDangerous = @"✅ Expected and usually NOT dangerous:
• Jaw pain 1-3 days after dose (temporary)
• Tingling in fingers/toes
• Temporary voice weakness or hoarseness
• Constipation (manageable with stool softeners)
• Fatigue next day",
            BloodCountPattern = "Does NOT typically cause significant drops in blood counts (this is unusual for chemotherapy)",
            IsCurrentlyRelevant = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            SideEffects = new List<DrugSideEffect>
            {
                new() { Id = Guid.NewGuid(), DrugId = vincristineId, EffectName = "Peripheral Neuropathy", Severity = "Common", Description = "Tingling, numbness in fingers/toes", TypicalOnset = "1-2 weeks" },
                new() { Id = Guid.NewGuid(), DrugId = vincristineId, EffectName = "Constipation", Severity = "Common", Description = "Slowed bowel movements", TypicalOnset = "Within days" },
                new() { Id = Guid.NewGuid(), DrugId = vincristineId, EffectName = "Jaw Pain", Severity = "Common", Description = "Temporary jaw discomfort", TypicalOnset = "1-3 days" },
                new() { Id = Guid.NewGuid(), DrugId = vincristineId, EffectName = "Vocal Cord Weakness", Severity = "Moderate", Description = "Weak or absent voice (autonomic neuropathy)", TypicalOnset = "Days to weeks" },
                new() { Id = Guid.NewGuid(), DrugId = vincristineId, EffectName = "Swallowing Coordination Changes", Severity = "Moderate", Description = "Temporary difficulty coordinating swallowing", TypicalOnset = "Days to weeks" }
            }
        });

        // 2. Daunorubicin
        var daunorubicinId = Guid.NewGuid();
        drugs.Add(new Drug
        {
            Id = daunorubicinId,
            Name = "Daunorubicin",
            DrugClass = "Anthracycline (IV infusion - the 'red drug')",
            MechanismOfAction = "Intercalates DNA and inhibits topoisomerase II, causing DNA strand breaks",
            WhyUsedInLeukemia = "Directly damages leukemia cell DNA, causing rapid cell death",
            ParentFriendlyExplanation = "Daunorubicin is a red drug that gets inside cancer cells and breaks their DNA. It's one of the most powerful drugs used to kill leukemia cells quickly. Your child's urine may turn red/orange for 1-2 days - this is normal and harmless.",
            TypicalOnsetTiming = "Immediate effect; side effects within days",
            DurationOfEffects = "Cleared in 24-48 hours; heart effects are cumulative over lifetime",
            ExpectedLabChanges = "Severe bone marrow suppression: expect low WBC, ANC, platelets, hemoglobin 10-14 days after",
            NeurologicalImpacts = "Minimal direct neurological effects",
            TypicalTimeline = @"Day 0: IV infusion (30-60 minutes)
Day 1-2: Red/orange urine (harmless)
Day 1-3: Nausea/fatigue peak
Day 7-10: Blood counts start dropping
Day 10-14: NADIR (lowest blood counts)
Day 14-21: Counts begin recovering
Day 21-28: Recovery continues",
            MonitoringReason = @"• Cumulative heart exposure (lifetime dose limits)
• Bone marrow suppression timing
• Infection risk during nadir (days 10-14)
• Echocardiograms to monitor heart function",
            CommonButNotDangerous = @"✅ Expected and usually NOT dangerous:
• Red or orange urine for 1-2 days
• Sudden fatigue next day
• Nausea (managed with anti-nausea meds)
• Hair thinning/loss after 2-3 weeks
• Low energy during nadir week",
            BloodCountPattern = @"🧪 Blood Count Pattern:
Week 1: Counts may look OK
Week 2: Significant drop begins
Week 2-3: Lowest point (nadir)
Week 3-4: Recovery begins
Note: Hemoglobin drops even WITHOUT marrow leukemia",
            IsCurrentlyRelevant = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            SideEffects = new List<DrugSideEffect>
            {
                new() { Id = Guid.NewGuid(), DrugId = daunorubicinId, EffectName = "Bone Marrow Suppression", Severity = "Severe", Description = "Severe drop in all blood counts", TypicalOnset = "10-14 days" },
                new() { Id = Guid.NewGuid(), DrugId = daunorubicinId, EffectName = "Cardiotoxicity", Severity = "Severe", Description = "Heart muscle weakening (monitored with echocardiograms)", TypicalOnset = "Cumulative over doses" },
                new() { Id = Guid.NewGuid(), DrugId = daunorubicinId, EffectName = "Red/Orange Urine", Severity = "Common", Description = "Harmless urine discoloration", TypicalOnset = "Within 24 hours" },
                new() { Id = Guid.NewGuid(), DrugId = daunorubicinId, EffectName = "Nausea/Vomiting", Severity = "Common", Description = "Can be severe; managed with anti-nausea meds", TypicalOnset = "Hours to days" },
                new() { Id = Guid.NewGuid(), DrugId = daunorubicinId, EffectName = "Hair Loss", Severity = "Common", Description = "Complete hair loss expected", TypicalOnset = "2-3 weeks" }
            }
        });

        // 3. Doxorubicin
        var doxorubicinId = Guid.NewGuid();
        drugs.Add(new Drug
        {
            Id = doxorubicinId,
            Name = "Doxorubicin",
            DrugClass = "Anthracycline",
            MechanismOfAction = "Similar to daunorubicin: intercalates DNA and inhibits topoisomerase II",
            WhyUsedInLeukemia = "Kills leukemia cells by damaging their DNA",
            ParentFriendlyExplanation = "Doxorubicin (also called Adriamycin) is very similar to daunorubicin - another red drug that breaks cancer cell DNA. It causes red urine temporarily, which is normal.",
            TypicalOnsetTiming = "Immediate effect; side effects within days",
            DurationOfEffects = "Cleared in 24-48 hours; heart effects cumulative",
            ExpectedLabChanges = "Severe bone marrow suppression 10-14 days after treatment",
            NeurologicalImpacts = "Minimal",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            SideEffects = new List<DrugSideEffect>
            {
                new() { Id = Guid.NewGuid(), DrugId = doxorubicinId, EffectName = "Bone Marrow Suppression", Severity = "Severe", Description = "Low blood counts", TypicalOnset = "10-14 days" },
                new() { Id = Guid.NewGuid(), DrugId = doxorubicinId, EffectName = "Cardiotoxicity", Severity = "Severe", Description = "Heart damage with cumulative doses", TypicalOnset = "Cumulative" },
                new() { Id = Guid.NewGuid(), DrugId = doxorubicinId, EffectName = "Red Urine", Severity = "Common", Description = "Harmless red/orange urine", TypicalOnset = "24 hours" },
                new() { Id = Guid.NewGuid(), DrugId = doxorubicinId, EffectName = "Mucositis", Severity = "Common", Description = "Mouth sores", TypicalOnset = "5-10 days" }
            }
        });

        // 4. Methotrexate
        var methotrexateId = Guid.NewGuid();
        drugs.Add(new Drug
        {
            Id = methotrexateId,
            Name = "Methotrexate",
            DrugClass = "Antimetabolite",
            MechanismOfAction = "Blocks folic acid metabolism, preventing DNA synthesis",
            WhyUsedInLeukemia = "Stops cancer cells from making DNA by blocking a vitamin they need",
            ParentFriendlyExplanation = "Methotrexate blocks folic acid (a vitamin) that cancer cells need to grow. Without it, they can't build DNA and die. We give leucovorin (rescue drug) to protect healthy cells. Can be given IV, by mouth, or into the spinal fluid (intrathecal).",
            TypicalOnsetTiming = "Works within hours; side effects 3-7 days later",
            DurationOfEffects = "Cleared in 24-48 hours with normal kidneys",
            ExpectedLabChanges = "Drops in WBC, platelets, hemoglobin 7-14 days after",
            NeurologicalImpacts = "High-dose or intrathecal can rarely cause confusion, seizures, or leukoencephalopathy",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            SideEffects = new List<DrugSideEffect>
            {
                new() { Id = Guid.NewGuid(), DrugId = methotrexateId, EffectName = "Mucositis", Severity = "Common", Description = "Mouth sores", TypicalOnset = "3-7 days" },
                new() { Id = Guid.NewGuid(), DrugId = methotrexateId, EffectName = "Bone Marrow Suppression", Severity = "Severe", Description = "Low blood counts", TypicalOnset = "7-14 days" },
                new() { Id = Guid.NewGuid(), DrugId = methotrexateId, EffectName = "Liver Toxicity", Severity = "Moderate", Description = "Elevated liver enzymes", TypicalOnset = "During treatment" },
                new() { Id = Guid.NewGuid(), DrugId = methotrexateId, EffectName = "Kidney Toxicity", Severity = "Moderate", Description = "Can damage kidneys at high doses", TypicalOnset = "During treatment" }
            }
        });

        // 5. Cytarabine (Ara-C)
        var cytarabineId = Guid.NewGuid();
        drugs.Add(new Drug
        {
            Id = cytarabineId,
            Name = "Cytarabine",
            DrugClass = "Antimetabolite",
            MechanismOfAction = "Inhibits DNA synthesis by interfering with DNA polymerase",
            WhyUsedInLeukemia = "Stops cancer cells from copying their DNA",
            ParentFriendlyExplanation = "Cytarabine (also called Ara-C) tricks cancer cells by looking like a building block of DNA. When cancer cells try to use it, their DNA breaks and they die. Given IV or into spinal fluid.",
            TypicalOnsetTiming = "Works within hours; side effects within days",
            DurationOfEffects = "Cleared quickly; effects last days",
            ExpectedLabChanges = "Severe bone marrow suppression 7-14 days after",
            NeurologicalImpacts = "High-dose can cause cerebellar toxicity (balance/coordination problems)",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            SideEffects = new List<DrugSideEffect>
            {
                new() { Id = Guid.NewGuid(), DrugId = cytarabineId, EffectName = "Bone Marrow Suppression", Severity = "Severe", Description = "Severe drop in blood counts", TypicalOnset = "7-14 days" },
                new() { Id = Guid.NewGuid(), DrugId = cytarabineId, EffectName = "Nausea/Vomiting", Severity = "Common", Description = "Managed with anti-nausea meds", TypicalOnset = "Hours to days" },
                new() { Id = Guid.NewGuid(), DrugId = cytarabineId, EffectName = "Flu-like Symptoms", Severity = "Common", Description = "Fever, body aches", TypicalOnset = "During infusion" },
                new() { Id = Guid.NewGuid(), DrugId = cytarabineId, EffectName = "Cerebellar Toxicity", Severity = "Moderate", Description = "Balance/coordination issues (high-dose)", TypicalOnset = "During treatment" }
            }
        });

        // 6. Cyclophosphamide
        var cyclophosphamideId = Guid.NewGuid();
        drugs.Add(new Drug
        {
            Id = cyclophosphamideId,
            Name = "Cyclophosphamide",
            DrugClass = "Alkylating Agent",
            MechanismOfAction = "Cross-links DNA strands, preventing cell division",
            WhyUsedInLeukemia = "Damages cancer cell DNA so they can't divide",
            ParentFriendlyExplanation = "Cyclophosphamide attaches to cancer cell DNA and tangles it up so the cells can't divide. It requires lots of IV fluids and a bladder-protecting drug (Mesna) to prevent bladder irritation.",
            TypicalOnsetTiming = "Works within hours; side effects within days",
            DurationOfEffects = "Cleared in 24-48 hours",
            ExpectedLabChanges = "Bone marrow suppression 7-14 days after",
            NeurologicalImpacts = "Minimal",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            SideEffects = new List<DrugSideEffect>
            {
                new() { Id = Guid.NewGuid(), DrugId = cyclophosphamideId, EffectName = "Bone Marrow Suppression", Severity = "Severe", Description = "Low blood counts", TypicalOnset = "7-14 days" },
                new() { Id = Guid.NewGuid(), DrugId = cyclophosphamideId, EffectName = "Hemorrhagic Cystitis", Severity = "Moderate", Description = "Bladder irritation/bleeding (prevented with Mesna)", TypicalOnset = "During/after treatment" },
                new() { Id = Guid.NewGuid(), DrugId = cyclophosphamideId, EffectName = "Nausea/Vomiting", Severity = "Common", Description = "Can be severe", TypicalOnset = "Hours to days" },
                new() { Id = Guid.NewGuid(), DrugId = cyclophosphamideId, EffectName = "Hair Loss", Severity = "Common", Description = "Temporary hair loss", TypicalOnset = "2-3 weeks" }
            }
        });

        // 7. Asparaginase
        var asparaginaseId = Guid.NewGuid();
        drugs.Add(new Drug
        {
            Id = asparaginaseId,
            Name = "Asparaginase (PEG-asparaginase/Erwinia)",
            DrugClass = "Enzyme (IM injection or IV infusion)",
            MechanismOfAction = "Depletes asparagine (amino acid) that leukemia cells need to survive",
            WhyUsedInLeukemia = "Starves leukemia cells by removing an amino acid they can't make themselves",
            ParentFriendlyExplanation = "Asparaginase is an enzyme that removes asparagine (a protein building block) from the blood. Normal cells can make their own asparagine, but leukemia cells can't - so they starve and die. Given as injection (IM) or IV infusion. Can cause allergic reactions.",
            TypicalOnsetTiming = "Works within hours; effects last 2-3 weeks",
            DurationOfEffects = "Long-acting: effects persist for weeks (PEG form)",
            ExpectedLabChanges = "Can affect liver function, clotting factors, blood sugar",
            NeurologicalImpacts = "Can rarely cause confusion, seizures, or stroke (due to clotting issues)",
            TypicalTimeline = @"Day 0: IM injection or IV infusion
Day 1-3: Watch for allergic reaction
Day 3-7: Liver enzymes may rise
Day 7-14: Clotting factors affected
Weeks 2-3: Effects persist
Note: PEG form lasts longer than Erwinia",
            MonitoringReason = @"• Allergic reactions (can be severe)
• Pancreatitis (severe abdominal pain)
• Liver toxicity (elevated enzymes)
• Blood clotting issues (both clots and bleeding)
• Blood sugar monitoring
🧪 Monitored Labs:
• Amylase/Lipase (pancreatitis)
• Fibrinogen & clotting factors
• Liver function tests",
            CommonButNotDangerous = @"✅ Expected and usually NOT dangerous:
• Mild nausea
• Fatigue
• Mild fever
• Injection site soreness (IM)
⚠️ Call team immediately for:
• Severe abdominal pain
• Rash or breathing difficulty
• Unusual bruising/bleeding",
            BloodCountPattern = "Does not directly suppress bone marrow, but affects clotting factors",
            IsCurrentlyRelevant = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            SideEffects = new List<DrugSideEffect>
            {
                new() { Id = Guid.NewGuid(), DrugId = asparaginaseId, EffectName = "Allergic Reactions", Severity = "Moderate", Description = "Rash, breathing difficulty, anaphylaxis", TypicalOnset = "During/after infusion" },
                new() { Id = Guid.NewGuid(), DrugId = asparaginaseId, EffectName = "Pancreatitis", Severity = "Severe", Description = "Severe abdominal pain", TypicalOnset = "Days to weeks" },
                new() { Id = Guid.NewGuid(), DrugId = asparaginaseId, EffectName = "Liver Toxicity", Severity = "Moderate", Description = "Elevated liver enzymes", TypicalOnset = "Days to weeks" },
                new() { Id = Guid.NewGuid(), DrugId = asparaginaseId, EffectName = "Blood Clotting Issues", Severity = "Severe", Description = "Increased clotting or bleeding risk", TypicalOnset = "During treatment" },
                new() { Id = Guid.NewGuid(), DrugId = asparaginaseId, EffectName = "High Blood Sugar", Severity = "Moderate", Description = "Temporary diabetes", TypicalOnset = "During treatment" }
            }
        });

        // 8. Mercaptopurine (6-MP)
        var mercaptopurineId = Guid.NewGuid();
        drugs.Add(new Drug
        {
            Id = mercaptopurineId,
            Name = "Mercaptopurine",
            DrugClass = "Antimetabolite",
            MechanismOfAction = "Interferes with purine synthesis, blocking DNA/RNA production",
            WhyUsedInLeukemia = "Stops cancer cells from making DNA building blocks",
            ParentFriendlyExplanation = "Mercaptopurine (6-MP) is a daily pill taken at home during maintenance therapy. It blocks cancer cells from making DNA. Take on empty stomach (1 hour before or 2 hours after food). Avoid dairy products which reduce absorption.",
            TypicalOnsetTiming = "Gradual effect over days to weeks",
            DurationOfEffects = "Cleared daily; cumulative effect",
            ExpectedLabChanges = "Gradual bone marrow suppression; monitored weekly",
            NeurologicalImpacts = "Minimal",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            SideEffects = new List<DrugSideEffect>
            {
                new() { Id = Guid.NewGuid(), DrugId = mercaptopurineId, EffectName = "Bone Marrow Suppression", Severity = "Moderate", Description = "Gradual drop in blood counts", TypicalOnset = "Weeks" },
                new() { Id = Guid.NewGuid(), DrugId = mercaptopurineId, EffectName = "Liver Toxicity", Severity = "Moderate", Description = "Elevated liver enzymes", TypicalOnset = "Weeks to months" },
                new() { Id = Guid.NewGuid(), DrugId = mercaptopurineId, EffectName = "Nausea", Severity = "Common", Description = "Mild nausea", TypicalOnset = "Hours after dose" },
                new() { Id = Guid.NewGuid(), DrugId = mercaptopurineId, EffectName = "Increased Infection Risk", Severity = "Moderate", Description = "Due to low WBC", TypicalOnset = "Ongoing" }
            }
        });

        // 9. Thioguanine (6-TG)
        var thioguanineId = Guid.NewGuid();
        drugs.Add(new Drug
        {
            Id = thioguanineId,
            Name = "Thioguanine",
            DrugClass = "Antimetabolite",
            MechanismOfAction = "Similar to mercaptopurine: blocks purine synthesis",
            WhyUsedInLeukemia = "Stops cancer cells from making DNA",
            ParentFriendlyExplanation = "Thioguanine (6-TG) is similar to mercaptopurine but used during certain phases of treatment. It's a pill that blocks cancer cells from building DNA. Take with food to reduce stomach upset.",
            TypicalOnsetTiming = "Gradual effect over days",
            DurationOfEffects = "Cleared daily",
            ExpectedLabChanges = "Bone marrow suppression",
            NeurologicalImpacts = "Minimal",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            SideEffects = new List<DrugSideEffect>
            {
                new() { Id = Guid.NewGuid(), DrugId = thioguanineId, EffectName = "Bone Marrow Suppression", Severity = "Moderate", Description = "Low blood counts", TypicalOnset = "Days to weeks" },
                new() { Id = Guid.NewGuid(), DrugId = thioguanineId, EffectName = "Liver Toxicity", Severity = "Moderate", Description = "Elevated liver enzymes", TypicalOnset = "Weeks" },
                new() { Id = Guid.NewGuid(), DrugId = thioguanineId, EffectName = "Nausea/Vomiting", Severity = "Common", Description = "Stomach upset", TypicalOnset = "Hours after dose" }
            }
        });

        // 10. Corticosteroids (Dexamethasone/Prednisone)
        var corticosteroidsId = Guid.NewGuid();
        drugs.Add(new Drug
        {
            Id = corticosteroidsId,
            Name = "Corticosteroids (Dexamethasone/Prednisone)",
            DrugClass = "Steroid (oral or IV)",
            MechanismOfAction = "Causes apoptosis (programmed death) in lymphoid cells",
            WhyUsedInLeukemia = "Directly kills leukemia cells and reduces inflammation",
            ParentFriendlyExplanation = "Dexamethasone (Decadron) or Prednisone are steroids that kill leukemia cells and reduce swelling. They cause increased appetite, mood changes, trouble sleeping, and high blood sugar. Effects reverse when stopped. Usually tapered gradually depending on protocol phase.",
            TypicalOnsetTiming = "Works within hours",
            DurationOfEffects = "Effects last hours; cleared daily",
            ExpectedLabChanges = "Can increase WBC temporarily (not a sign of leukemia), raise blood sugar",
            NeurologicalImpacts = "Mood changes, insomnia, behavioral changes, increased energy, irritability",
            TypicalTimeline = @"Day 1-2: Appetite increases dramatically
Day 2-3: Sleep disruption begins
Day 3-7: Mood/behavior changes peak
During treatment: High energy, emotional swings
After taper: Gradual return to baseline
Note: Effects are dose-dependent",
            MonitoringReason = @"• Blood sugar levels (temporary diabetes risk)
• Mood and behavioral changes
• Infection risk (immune suppression)
• Blood pressure
• Weight gain",
            CommonButNotDangerous = @"✅ Expected and usually NOT dangerous:
• Extreme hunger and food-seeking
• Difficulty sleeping at night
• Increased energy/hyperactivity
• Emotional ups and downs
• Puffy cheeks (moon face) after weeks
• Weight gain",
            BloodCountPattern = "May temporarily INCREASE WBC count (this is a steroid effect, not leukemia progression)",
            IsCurrentlyRelevant = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            SideEffects = new List<DrugSideEffect>
            {
                new() { Id = Guid.NewGuid(), DrugId = corticosteroidsId, EffectName = "Increased Appetite", Severity = "Common", Description = "Extreme hunger, weight gain", TypicalOnset = "Within days" },
                new() { Id = Guid.NewGuid(), DrugId = corticosteroidsId, EffectName = "Mood Changes", Severity = "Common", Description = "Irritability, emotional swings", TypicalOnset = "Within days" },
                new() { Id = Guid.NewGuid(), DrugId = corticosteroidsId, EffectName = "Insomnia", Severity = "Common", Description = "Difficulty sleeping", TypicalOnset = "Within days" },
                new() { Id = Guid.NewGuid(), DrugId = corticosteroidsId, EffectName = "High Blood Sugar", Severity = "Moderate", Description = "Temporary diabetes", TypicalOnset = "During treatment" },
                new() { Id = Guid.NewGuid(), DrugId = corticosteroidsId, EffectName = "Increased Infection Risk", Severity = "Moderate", Description = "Suppresses immune system", TypicalOnset = "During treatment" },
                new() { Id = Guid.NewGuid(), DrugId = corticosteroidsId, EffectName = "Facial Swelling", Severity = "Common", Description = "Puffy cheeks (moon face)", TypicalOnset = "After weeks" }
            }
        });

        // 11. Blinatumomab
        var blinatumomabId = Guid.NewGuid();
        drugs.Add(new Drug
        {
            Id = blinatumomabId,
            Name = "Blinatumomab",
            DrugClass = "Bispecific T-cell Engager",
            MechanismOfAction = "Connects T-cells to B-cell leukemia cells, causing T-cells to kill them",
            WhyUsedInLeukemia = "Uses the body's own immune cells to find and destroy leukemia cells",
            ParentFriendlyExplanation = "Blinatumomab is a newer immunotherapy drug that acts like a bridge connecting your child's T-cells (immune cells) to leukemia cells. Once connected, the T-cells attack and kill the leukemia. Given as continuous IV infusion over 28 days through a special pump. Requires hospital admission for first 1-3 days to monitor for side effects.",
            TypicalOnsetTiming = "Works within hours; monitored closely for first 48-72 hours",
            DurationOfEffects = "Active during continuous infusion (28 days)",
            ExpectedLabChanges = "Can cause cytokine release (fever, low blood pressure), low blood counts",
            NeurologicalImpacts = "Can cause confusion, tremors, seizures, speech difficulty (usually reversible)",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            SideEffects = new List<DrugSideEffect>
            {
                new() { Id = Guid.NewGuid(), DrugId = blinatumomabId, EffectName = "Cytokine Release Syndrome", Severity = "Severe", Description = "Fever, low blood pressure, breathing difficulty", TypicalOnset = "First 48 hours" },
                new() { Id = Guid.NewGuid(), DrugId = blinatumomabId, EffectName = "Neurological Effects", Severity = "Moderate", Description = "Confusion, tremors, speech difficulty, seizures", TypicalOnset = "First week" },
                new() { Id = Guid.NewGuid(), DrugId = blinatumomabId, EffectName = "Infections", Severity = "Moderate", Description = "Increased infection risk", TypicalOnset = "During treatment" },
                new() { Id = Guid.NewGuid(), DrugId = blinatumomabId, EffectName = "Fever", Severity = "Common", Description = "Fever without infection", TypicalOnset = "Throughout treatment" },
                new() { Id = Guid.NewGuid(), DrugId = blinatumomabId, EffectName = "Headache", Severity = "Common", Description = "Headaches", TypicalOnset = "During treatment" }
            }
        });

        return drugs;
    }
}
