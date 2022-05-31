--Part 1

DESCRIBE jobs;

--Part 2
SELECT * FROM employers
WHERE location = "St. Louis City ";

--Part 3
SELECT name, description
FROM skills
LEFT JOIN jobskills ON skills.Id = jobskills.SkillId
WHERE jobskills.JobId IS NOT NULL;