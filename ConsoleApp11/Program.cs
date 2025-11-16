using System;
using System.Collections.Generic;
using ConsoleApp11;
using ConsoleApp11.Models;

namespace ConsoleApp11
{
    class Program
    {
        static List<TaskItem> tasks = new List<TaskItem>();
        static EmailService emailService;
        static string userEmail = "";
        static string appPassword = "";

        static void Main(string[] args)
        {
            Console.WriteLine("=== 이메일 설정 ===");
            Console.Write("알림을 받을 이메일 주소 입력: ");
            userEmail = Console.ReadLine();

            Console.Write("이메일 앱 비밀번호 입력(구글 16자리): ");
            appPassword = Console.ReadLine();

            emailService = new EmailService(userEmail, appPassword);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== 과제 마감 알리미 =====");
                Console.WriteLine("1. 과제 추가");
                Console.WriteLine("2. 과제 목록 보기");
                Console.WriteLine("3. 마감 하루 전 알림 확인 & 이메일 발송");
                Console.WriteLine("0. 종료");
                Console.Write("메뉴 선택: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddTask();
                        break;

                    case "2":
                        ShowTasks();
                        break;

                    case "3":
                        CheckNotifications();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void AddTask()
        {
            Console.Clear();
            Console.WriteLine("=== 과제 추가 ===");

            Console.Write("과제 제목: ");
            string title = Console.ReadLine();

            Console.Write("메모: ");
            string memo = Console.ReadLine();

            Console.Write("마감 날짜 (YYYY-MM-DD): ");
            DateTime dueDate;

            while (!DateTime.TryParse(Console.ReadLine(), out dueDate))
            {
                Console.Write("날짜 형식이 잘못됨. 다시 입력해 주세요: ");
            }

            tasks.Add(new TaskItem
            {
                Title = title,
                Memo = memo,
                DueDate = dueDate
            });

            Console.WriteLine("과제가 성공적으로 추가되었습니다!");
            Console.ReadKey();
        }

        static void ShowTasks()
        {
            Console.Clear();
            Console.WriteLine("=== 과제 목록 ===");

            if (tasks.Count == 0)
            {
                Console.WriteLine("등록된 과제가 없습니다.");
            }
            else
            {
                foreach (var t in tasks)
                {
                    Console.WriteLine($"- 제목: {t.Title}");
                    Console.WriteLine($"  메모: {t.Memo}");
                    Console.WriteLine($"  마감 날짜: {t.DueDate:yyyy-MM-dd}");
                    Console.WriteLine($"  남은 날짜: {t.DaysLeft()}일\n");
                }
            }

            Console.ReadKey();
        }

        static void CheckNotifications()
        {
            Console.Clear();
            Console.WriteLine("=== 마감 하루 전 알림 ===");

            bool found = false;

            foreach (var t in tasks)
            {
                if (t.IsDueTomorrow())
                {
                    Console.WriteLine($"[알림] '{t.Title}' 과제가 내일 마감입니다!");

                    string subject = $"[과제 알림] '{t.Title}' 마감 하루 전!";
                    string body =
                        $"과제 제목: {t.Title}\n" +
                        $"메모: {t.Memo}\n" +
                        $"마감 날짜: {t.DueDate:yyyy-MM-dd}\n" +
                        $"남은 시간: 1일\n";

                    emailService.SendMail(userEmail, subject, body);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("하루 남은 과제가 없습니다.");
            }

            Console.ReadKey();
        }
    }
}

