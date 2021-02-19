using ParserAPI.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ParserAPI.Core
{
    public class SectionTypeAndIndexRepositoryBuilder : ISectionTypeAndIndexRepositoryBuilder
    {
        IDelimiterRepository _delimiterRepository;
        public SectionTypeAndIndexRepositoryBuilder(IDelimiterRepository delimiterRepository)
        {
            _delimiterRepository = delimiterRepository;
        }

        public KeyValuePair<int, string> FindAddressSection(List<string> textList)
        {
            #region Delimiter check
            int addressSectionIndex = textList.FindIndex(a => a.Equals("ADDRESS:") || a.Equals("Address:") || a.Equals("address:") ||
                            a.Equals("ADDRESS-") || a.Equals("Address-") || a.Equals("address-") ||
                            a.Equals("ADDRESS") || a.Equals("Address") || a.Equals("address") ||
                            a.Contains("ADDRESS") || a.Contains("Address") || a.Contains("address")
                        );
            #endregion

            return new KeyValuePair<int, string>(addressSectionIndex, "address");
        }
        public KeyValuePair<int, string> FindCertificationSection(List<string> textList)
        {
            #region Delimiter check
            int certificationSectionIndex = textList.FindIndex(a => a.Equals("CERTIFICATIONS:") || a.Equals("Certifications:") || a.Equals("certifications:") ||
                            a.Equals("CERTIFICATIONS-") || a.Equals("Certifications-") || a.Equals("certifications-") ||
                            a.Equals("CERTIFICATIONS") || a.Equals("Certifications") || a.Equals("certifications") ||
                            a.Equals("CERTIFICATES") || a.Equals("Certificates") || a.Equals("certificates") ||
                            a.Equals("CERTIFICATES:") || a.Equals("Certificates:") || a.Equals("certificates:") ||
                            a.Equals("CERTIFICATES-") || a.Equals("Certificates-") || a.Equals("certificates-") ||
                            a.Contains("CERTIFICATIONS") || a.Contains("Certifications") || a.Contains("certifications")
                        );
            #endregion

            return new KeyValuePair<int, string>(certificationSectionIndex, "certifications");
        }
        public KeyValuePair<int, string> FindEducationSection(List<string> textList)
        {
            var container = _delimiterRepository.GetDegrees();
            var degrees = new List<string>();

            foreach (var degreeList in container)
            {
                degrees.AddRange(degreeList.Degrees.Select(d => !degrees.Contains(d.Degree) ? d.Degree : ""));
            }

            degrees.RemoveAll(d => d.Equals(""));

            var potentialEducationSections = new Dictionary<int, string>();

            for (int i = 0; i < textList.Count; i++)
            {
                var line = textList[i];
                #region Delimiter check
                if (line.Equals("EDUCATION:") || line.Equals("Education:") || line.Equals("education:") ||
                                line.Equals("EDUCATION-") || line.Equals("Education-") || line.Equals("education-") ||
                                line.Equals("EDUCATION") || line.Equals("Education") || line.Equals("education") ||
                                line.Equals("EDUCATION HISTORY:") || line.Equals("Education History:") || line.Equals("education history:") ||
                                line.Equals("EDUCATION HISTORY-") || line.Equals("Education History-") || line.Equals("education history-") ||
                                line.Equals("EDUCATION HISTORY") || line.Equals("Education History") || line.Equals("education history") ||
                                line.Equals("DEGREES:") || line.Equals("Degrees:") || line.Equals("degrees:") ||
                                line.Equals("DEGREES-") || line.Equals("Degrees-") || line.Equals("degrees-") ||
                                line.Equals("DEGREES") || line.Equals("Degrees") || line.Equals("degrees") ||
                                line.Equals("TRAINING / EDUCATION:") || line.Equals("Training / Education:") || line.Equals("training / education:") ||
                                line.Equals("TRAINING / EDUCATION-") || line.Equals("Training / Education-") || line.Equals("training / education-") ||
                                line.Equals("TRAINING / EDUCATION") || line.Equals("Training / Education") || line.Equals("training / education") ||
                                line.Equals("TRAINING/EDUCATION:") || line.Equals("Training/Education:") || line.Equals("training/education:") ||
                                line.Equals("TRAINING/EDUCATION-") || line.Equals("Training/Education-") || line.Equals("training/education-") ||
                                line.Equals("TRAINING/EDUCATION") || line.Equals("Training/Education") || line.Equals("training/education")
                            )
                {
                    potentialEducationSections.Add(i, line);
                }
                #endregion
            }

            return new KeyValuePair<int, string>(GetIndexInRange(potentialEducationSections, textList, degrees), "education");
        }
        public KeyValuePair<int, string> FindEmploymentSection(List<string> textList)
        {
            var companyNames = _delimiterRepository.GetCompanyNames();
            var titles = _delimiterRepository.GetJobTitles();
            var potentialEmploymentSections = new Dictionary<int, string>();
            var employmentTitleAndIndex = new Dictionary<int, string>();
            var regex = new Regex("[^a-zA-Z ]");

            for (int i = 0; i < textList.Count; i++)
            {
                var line = textList[i];
                #region Delimiter check
                if (
                                   line.Equals("EMPLOYMENT:") || line.Equals("Employment:") || line.Equals("employment:") ||
                                   line.Equals("EMPLOYMENT-") || line.Equals("Employment-") || line.Equals("employment-") ||                                   

                                   line.Equals("PROFESSIONAL EMPLOYMENT:") || line.Equals("Professional Employment:") || line.Equals("professional employment:") ||
                                   line.Equals("PROFESSIONAL EMPLOYMENT-") || line.Equals("Professional Employment-") || line.Equals("professional employment-") ||
                                   line.Equals("PROFESSIONAL EMPLOYMENT") || line.Equals("Professional Employment") || line.Equals("professional employment") ||

                                   line.Equals("EMPLOYMENT HISTORY:") || line.Equals("Employment History:") || line.Equals("employment history:") ||
                                   line.Equals("EMPLOYMENT HISTORY-") || line.Equals("Employment History-") || line.Equals("employment history-") ||
                                   line.Equals("EMPLOYMENT HISTORY") || line.Equals("Employment History") || line.Equals("employment history") ||

                                   line.Equals("PROFESSIONAL EMPLOYMENT HISTORY:") || line.Equals("Professional Employment History:") || line.Equals("professional employment history:") ||
                                   line.Equals("PROFESSIONAL EMPLOYMENT HISTORY-") || line.Equals("Professional Employment History-") || line.Equals("professional employment history-") ||
                                   line.Equals("PROFESSIONAL EMPLOYMENT HISTORY") || line.Equals("Professional Employment History") || line.Equals("professional employment history") ||

                                   line.Equals("PROFESSIONAL HISTORY:") || line.Equals("Professional History:") || line.Equals("professional history:") ||
                                   line.Equals("PROFESSIONAL HISTORY-") || line.Equals("Professional History-") || line.Equals("professional history-") ||
                                   line.Equals("PROFESSIONAL HISTORY") || line.Equals("Professional History") || line.Equals("professional history") ||

                                   line.Equals("EXPERIENCE:") || line.Equals("Experience:") || line.Equals("experience:") ||
                                   line.Equals("EXPERIENCE-") || line.Equals("Experience-") || line.Equals("experience-") ||                                   

                                   line.Equals("PROFESSIONAL EXPERIENCE:") || line.Equals("Professional Experience:") || line.Equals("professional experience:") ||
                                   line.Equals("PROFESSIONAL EXPERIENCE-") || line.Equals("Professional Experience-") || line.Equals("professional experience-") ||
                                   line.Equals("PROFESSIONAL EXPERIENCE") || line.Equals("Professional Experience") || line.Equals("professional experience") ||

                                   line.Equals("JOBS:") || line.Equals("Jobs:") || line.Equals("jobs:") ||
                                   line.Equals("JOBS-") || line.Equals("Jobs-") || line.Equals("jobs-") ||
                                   
                                   line.Equals("PROFESSIONAL JOBS:") || line.Equals("Professional Jobs:") || line.Equals("professional jobs:") ||
                                   line.Equals("PROFESSIONAL JOBS-") || line.Equals("Professional Jobs-") || line.Equals("professional jobs-") ||
                                   line.Equals("PROFESSIONAL JOBS") || line.Equals("Professional Jobs") || line.Equals("professional jobs") ||

                                   line.Equals("JOB HISTORY:") || line.Equals("Job History:") || line.Equals("job history:") ||
                                   line.Equals("JOB HISTORY-") || line.Equals("Job History-") || line.Equals("job history-") ||
                                   line.Equals("JOB HISTORY") || line.Equals("Job History") || line.Equals("job history") ||

                                   line.Equals("PROFESSIONAL JOB HISTORY:") || line.Equals("Professional Job History:") || line.Equals("professional job history:") ||
                                   line.Equals("PROFESSIONAL JOB HISTORY-") || line.Equals("Professional Job History-") || line.Equals("professional job history-") ||
                                   line.Equals("PROFESSIONAL JOB HISTORY") || line.Equals("Professional Job History") || line.Equals("professional job history") ||

                                   line.Equals("CAREER HISTORY:") || line.Equals("Career History:") || line.Equals("career history:") ||
                                   line.Equals("CAREER HISTORY-") || line.Equals("Career History-") || line.Equals("career history-") ||
                                   line.Equals("CAREER HISTORY") || line.Equals("Career History") || line.Equals("career history") ||

                                   line.Equals("PROFESSIONAL CAREER HISTORY:") || line.Equals("Professional Career History:") || line.Equals("professional career history:") ||
                                   line.Equals("PROFESSIONAL CAREER HISTORY-") || line.Equals("Professional Career History-") || line.Equals("professional career history-") ||
                                   line.Equals("PROFESSIONAL CAREER HISTORY") || line.Equals("Professional Career History") || line.Equals("professional career history") ||

                                   line.Equals("CAREER:") || line.Equals("Career:") || line.Equals("career:") ||
                                   line.Equals("CAREER-") || line.Equals("Career-") || line.Equals("career-") ||                                   

                                   line.Equals("CLIENT PROJECTS:") || line.Equals("Client Projects:") || line.Equals("client projects:") ||
                                   line.Equals("CLIENT PROJECTS-") || line.Equals("Client Projects-") || line.Equals("client projects-") ||
                                   line.Equals("CLIENT PROJECTS") || line.Equals("Client Projects") || line.Equals("client projects") ||

                                   line.Equals("PROFESSIONAL CAREER:") || line.Equals("Professional Career:") || line.Equals("professional career:") ||
                                   line.Equals("PROFESSIONAL CAREER-") || line.Equals("Professional Career-") || line.Equals("professional career-") ||
                                   line.Equals("PROFESSIONAL CAREER") || line.Equals("Professional Career") || line.Equals("professional career") ||

                                   line.Equals("WORK EXPERIENCE:") || line.Equals("Work Experience:") || line.Equals("work experience:") ||
                                   line.Equals("WORK EXPERIENCE-") || line.Equals("Work Experience-") || line.Equals("work experience-") ||
                                   line.Equals("WORK EXPERIENCE") || line.Equals("Work Experience") || line.Equals("work experience") ||

                                   line.Equals("EMPLOYMENT") || line.Equals("Employment") || line.Equals("employment") ||
                                   line.Equals("CAREER") || line.Equals("Career") || line.Equals("career") ||
                                   line.Equals("JOBS") || line.Equals("Jobs") || line.Equals("jobs") ||
                                   line.Equals("EXPERIENCE") || line.Equals("Experience") || line.Equals("experience")
                               )
                {
                    potentialEmploymentSections.Add(i, line);
                }
                #endregion
            }

            return new KeyValuePair<int, string>(GetIndexInRange(potentialEmploymentSections, textList, titles), "employment");
        }
        public KeyValuePair<int, string> FindSkillsSection(List<string> textList)
        {
            var potentialSkillSections = new Dictionary<int, string>();

            for (int i = 0; i < textList.Count; i++)
            {
                var line = textList[i];

                #region Delimiter check
                if (
                                    line.Equals("SKILLS:") || line.Equals("Skills:") || line.Equals("skills:") ||
                                    line.Equals("SKILLS-") || line.Equals("Skills-") || line.Equals("skills-") ||
                                    line.Equals("SKILLS") || line.Equals("Skills") || line.Equals("skills") ||

                                    line.Equals("SKILLS SECTION:") || line.Equals("Skills Section:") || line.Equals("skills section:") ||
                                    line.Equals("SKILLS SECTION-") || line.Equals("Skills Section-") || line.Equals("skills section-") ||
                                    line.Equals("SKILLS SECTION") || line.Equals("Skills Section") || line.Equals("skills section") ||

                                    line.Equals("SKILL SECTION:") || line.Equals("Skill Section:") || line.Equals("skill section:") ||
                                    line.Equals("SKILL SECTION-") || line.Equals("Skill Section-") || line.Equals("skill section-") ||
                                    line.Equals("SKILL SECTION") || line.Equals("Skill Section") || line.Equals("skill section") ||

                                    line.Equals("PROFESSIONAL SKILLS:") || line.Equals("Professional Skills:") || line.Equals("professional skills:") ||
                                    line.Equals("PROFESSIONAL SKILLS-") || line.Equals("Professional Skills-") || line.Equals("professional skills-") ||
                                    line.Equals("PROFESSIONAL SKILLS") || line.Equals("Professional Skills") || line.Equals("professional skills") ||

                                    line.Equals("TECHNICAL SKILLS:") || line.Equals("Technical Skills:") || line.Equals("technical skills:") ||
                                    line.Equals("TECHNICAL SKILLS-") || line.Equals("Technical Skills-") || line.Equals("technical skills-") ||
                                    line.Equals("TECHNICAL SKILLS") || line.Equals("Technical Skills") || line.Equals("technical skills") ||

                                    line.Equals("TECH SKILLS:") || line.Equals("Tech Skills:") || line.Equals("tech skills:") ||
                                    line.Equals("TECH SKILLS-") || line.Equals("Tech Skills-") || line.Equals("tech skills-") ||
                                    line.Equals("TECH SKILLS") || line.Equals("Tech Skills") || line.Equals("tech skills")
                               )
                {
                    potentialSkillSections.Add(i, line);
                }
                #endregion
            }

            var skillLists = _delimiterRepository.GetSkills();

            var skills = new List<string>(); 

            foreach(var skillList in skillLists)
            {
                skills.AddRange(skillList.Skills.Select(s => s.Name).ToList()); 
            }   
            
            if(potentialSkillSections.Count == 1 && textList.ElementAt(potentialSkillSections.ElementAt(0).Key).Split(" ").Length == 1)
            {
                return potentialSkillSections.ElementAt(0);
            }
            else
            {
                return new KeyValuePair<int, string>(GetIndexInRange(potentialSkillSections, textList, skills), "skills");
            }            
        }
        public KeyValuePair<int, string> FindSummarySection(List<string> textList)
        {
            #region Delimiter check
            int summarySectionIndex = textList.FindIndex(a => a.Equals("SUMMARY:") || a.Equals("Summary:") || a.Equals("summery:") ||
                            a.Equals("SUMMARY-") || a.Equals("Summary-") || a.Equals("summery-") ||
                            a.Equals("SUMMARY") || a.Equals("Summary") || a.Equals("summery") ||
                            a.Contains("SUMMARY") || a.Contains("Summary") || a.Contains("summery") ||
                            a.Equals("PROFESSIONAL SUMMARY") || a.Equals("Professional Summary") || a.Equals("professional summery") ||
                            a.Equals("PROFESSIONAL SUMMARY:") || a.Equals("Professional Summary:") || a.Equals("professional summery:") ||
                            a.Equals("PROFESSIONAL SUMMARY-") || a.Equals("Professional Summary-") || a.Equals("professional summery-") ||
                            a.Equals("PROFESSIONAL PROFILE:") || a.Equals("Professional Profile:") || a.Equals("professional profile:") ||
                            a.Equals("PROFESSIONAL PROFILE-") || a.Equals("Professional Profile-") || a.Equals("professional profile-") ||
                            a.Equals("SUMMARY OF EXPERIENCE") || a.Equals("Summary Of Experience") || a.Equals("summery of experience") ||
                            a.Equals("SUMMARY OF EXPERIENCE:") || a.Equals("Summary Of Experience:") || a.Equals("summery of experience:") ||
                            a.Equals("SUMMARY OF EXPERIENCE-") || a.Equals("Summary Of Experience-") || a.Equals("summery of experience-") ||
                            a.Equals("SUMMARY OF EXPERIENCE:") || a.Equals("Summary Of Experience:") || a.Equals("summery of experience:") ||
                            a.Contains("PROFILE") || a.Contains("Profile") || a.Contains("profile") ||
                            a.Equals("PROFESSIONAL PROFILE") || a.Equals("Professional Profile") || a.Equals("professional profile")
                        );
            #endregion

            return new KeyValuePair<int, string>(summarySectionIndex, "summary");
        }

        //Achievements 

        //Professional Objective

        private int GetIndexInRange(Dictionary<int, string> potentialSections, List<string> textList, List<string> containerOfItemsToCompare)
        {            
            //We want to return -1 if no section is found. This is an undeniable way to express this.
            //Returning sectionIndex as 0 doesnt cut it because it could mean that there is a section
            //found at 0 or that the section doesnt exist at all. 
            if(potentialSections.Count == 0)
            {
                return -1;
            }
            else if(potentialSections.Count == 1)
            {
                return potentialSections.First().Key;
            }

            var regex = new Regex("[^a-zA-Z ]");
            var sectionIndex = 0;

            foreach (var potentialLineContainingJobTitle in potentialSections)
            {
                var limit = 0;

                if (potentialLineContainingJobTitle.Key + 3 < textList.Count)
                {
                    limit = potentialLineContainingJobTitle.Key + 3;
                }
                else
                {
                    limit = potentialLineContainingJobTitle.Key;
                }

                for (var lineIndex = potentialLineContainingJobTitle.Key; lineIndex < limit; lineIndex++)
                {
                    var correctedLine = regex.Replace(textList[lineIndex], "");
                    var line = correctedLine.Split(' ').ToList();

                    foreach (var word in line)
                    {
                        if (containerOfItemsToCompare.Contains(word))
                        {
                            sectionIndex = potentialLineContainingJobTitle.Key;
                            break;
                        }

                        //since indexing starts at 0 and list counts start at 1, all the following indexOf lookups will be 1 more than expected
                        else if (line.IndexOf(word) + 6 <= line.Count &&
                            containerOfItemsToCompare.Any(s => s.Equals(word + " " + line.ElementAt(line.IndexOf(word) + 1)
                                                 + " " + line.ElementAt(line.IndexOf(word) + 2)
                                                 + " " + line.ElementAt(line.IndexOf(word) + 3)
                                                 + " " + line.ElementAt(line.IndexOf(word) + 4)
                                                 + " " + line.ElementAt(line.IndexOf(word) + 5), StringComparison.OrdinalIgnoreCase)))
                        {
                            sectionIndex = potentialLineContainingJobTitle.Key;
                            break;
                        }
                        else if (line.IndexOf(word) + 5 <= line.Count &&
                            containerOfItemsToCompare.Any(s => s.Equals(word + " " + line.ElementAt(line.IndexOf(word) + 1)
                                                 + " " + line.ElementAt(line.IndexOf(word) + 2)
                                                 + " " + line.ElementAt(line.IndexOf(word) + 3)
                                                 + " " + line.ElementAt(line.IndexOf(word) + 4), StringComparison.OrdinalIgnoreCase)))
                        {
                            sectionIndex = potentialLineContainingJobTitle.Key;
                            break;
                        }
                        else if (line.IndexOf(word) + 4 <= line.Count &&
                            containerOfItemsToCompare.Any(s => s.Equals(word + " " + line.ElementAt(line.IndexOf(word) + 1)
                                                 + " " + line.ElementAt(line.IndexOf(word) + 2)
                                                 + " " + line.ElementAt(line.IndexOf(word) + 3), StringComparison.OrdinalIgnoreCase)))
                        {
                            sectionIndex = potentialLineContainingJobTitle.Key;
                            break;
                        }
                        else if (line.IndexOf(word) + 3 <= line.Count &&
                            containerOfItemsToCompare.Any(s => s.Equals(word + " " + line.ElementAt(line.IndexOf(word) + 1)
                                                 + " " + line.ElementAt(line.IndexOf(word) + 2), StringComparison.OrdinalIgnoreCase)))
                        {
                            sectionIndex = potentialLineContainingJobTitle.Key;
                            break;
                        }
                        else if (line.IndexOf(word) + 2 <= line.Count &&
                            containerOfItemsToCompare.Any(s => s.Equals(word + " " + line.ElementAt(line.IndexOf(word) + 1), StringComparison.OrdinalIgnoreCase)))
                        {
                            sectionIndex = potentialLineContainingJobTitle.Key;
                            break;
                        }
                    }
                }
            }

            return sectionIndex;
        }
    }
}