using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using ReviewProj.Domain.Entities;

namespace ReviewProj.Domain.Concrete
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext()
            : base("ReviewProj_2.3", throwIfV1Schema: false)
        {
            Database.SetInitializer(new DBInitializer());
            this.Configuration.LazyLoadingEnabled = true;
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        public virtual DbSet<Vote> Votes { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Reviewer> Reviewers { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Enterprise> Enterprises { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Ban> Bans { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
    }

    public class DBInitializer : DropCreateDatabaseAlways<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {

            #region Add Users
            //Add Owner
            context.Owners.Add(new Owner
            {
                Email = "vmaryniak@ukr.net",
                UserName = "Vova Maryniak",
                IsBanned = false
            });

            //Add Reviewer
            context.Reviewers.Add(new Reviewer
            {
                Email = "oleg_onyschak@gmail.com",
                UserName = "Oleg Onyschak",
                BirthDate = new DateTime(1971, 8, 10),
                RegistrationDate = new DateTime(2014, 2, 15)
            });

            context.SaveChanges();

            Owner owner = context.Owners.FirstOrDefaultAsync<Owner>().Result;
            Reviewer reviewer = context.Reviewers.FirstOrDefaultAsync<Reviewer>().Result;
            #endregion

            #region Add Enterprises

            List<Enterprise> Enterprises = new List<Enterprise>();

            Enterprises.Add(new Enterprise
            {
                Name = "Coffee Manufacture",
                Description = @"Кав’ярня «Львівська Мануфактура кави» дотримується ідеї, що кава — це не напій, це — культура, а культура має народжуватися і жити у справжній львівській атмосфері! Тут самостійно смажать та змішують найкращі світові сорти кави, утворюючи не просто напій, а особливий натхненний еліксир бадьорості! Дотримуючись основних канонів у приготуванні у «Львівській Мануфактурі» пригощають ще й почуттями. Кожна думка і настрій, які витають ланцюжком у головах — то своєрідний п’ятий елемент напою. 
 
Свіжо посмажена, свіжо змелена та свіжо зварена кава — ідеальна. Це основна ознака, яка вирізняє каву Мануфактури від інших. Майстерність смажити каву і готувати унікальні суміші — це велике ремесло, а працівники кав’ярні володіють знаннями і унікальними рецептурами, які винаходились роками. І нині, професійні кав’ярі віднайшли найкращі пропорції поєднання різних сортів в один бленд, де кожен сорт доповнює смак іншого, утворюючи унікальний букет. 
 
Наша справа — то є кава. То гаряча і гірка принада, в якій важко собі відмовити. І кожен, хто знається на цій благородній і поважній справі — майстер. І тіко чесна й добра філіжанка нами звареної кави — це відчутний прояв щонайменше хорошого тону і щирості у компліментах до людей, бо ми славимо та розвиваємо велику культуру кави і Вам бажаємо доєднатись і якомога швидше стати частиною спільноти, що розуміється на каві. ",
                Address = new Address
                {
                    City = "Lviv",
                    Street = "Rynok pl.",
                    HouseNumber = "10"
                },
                Contacts = new List<string>
                {
                    "+380 67 670 6106"
                },
                Resources = new List<Resource>
                {
                    new Resource { DataPath = "0001.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0002.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0003.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0004.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0005.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0006.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0007.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0008.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0009.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0010.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0011.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0012.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0013.png", Type = ResourceType.SecondaryImage, Format = ResourceFormat.PNG },
                    new Resource { DataPath = "0014.png", Type = ResourceType.SecondaryImage, Format = ResourceFormat.PNG },
                    new Resource { DataPath = "0015.jpg", Type = ResourceType.MainImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0016.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0017.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0018.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0019.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0020.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0021.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0022.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                },
                Type = EnterpriceType.Restaurant,
                Reviews = new List<Review>
                {
                    new Review
                    {
                        Description = "Very nice ambience! Efficient waiters. Tasty beer, tea, panacota. Alive band. Waiters speak English. English menu.",
                        Mark = 5,
                        Date = new DateTime(2017, 5, 7, 16, 34, 0),
                        TotalLikes = 2,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = "The place is a combination between souvenir shop, coffee store, cafe, and restaurant. The also have unique underground restaurant. I've been there twice for lunch and dinner. On some days, they have good live music.",
                        Mark = 4,
                        Date = new DateTime(2017, 5, 7, 12, 52, 0),
                        TotalLikes = 4,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = "Delicious coffee and desert in a beautiful cozy place. Everyone can find something he likes. I recomend to visit.",
                        Mark = 5,
                        Date = new DateTime(2017, 5, 5, 22, 54, 0),
                        TotalLikes = 6,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = "A place that has a number of coffee choices, including hot coffee cocktails. Deserts are good and the staff is friendly",
                        Mark = 5,
                        Date = new DateTime(2017, 5, 3, 11, 5, 0),
                        TotalLikes = 4,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = "Perfect place for cake and coffee. When you came in you smell aroma of different coffee. A lot of different in menu so everybody can choose something. There is also a shop when they sell coffee. We were satisfied.",
                        Mark = 5,
                        Date = new DateTime(2017, 5, 2, 19, 14, 0),
                        TotalLikes = 8,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = "I think this place is one of the famous and unusual for coffee lovers. Have visited this cafe/shop with excursion group and tried so-called sealed coffee, which is their specialty. It has an extraordinary idea for interior and spirit of fairy-tale",
                        Mark = 5,
                        Date = new DateTime(2017, 4, 28, 17, 41, 0),
                        TotalLikes = 11,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = "It is the reAl fun for educated person to hear how people around believe that the coffee is taken out of mine like coal. By the way the coffee itself is good enough.",
                        Mark = 5,
                        Date = new DateTime(2017, 2, 14, 16, 58, 0),
                        TotalLikes = 4,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = "Walked in to a warm and friendly environment with traditional coffee smells, paraphernalia and atmosphere. The staff were nice and quick to help. The coffee was good. Music in the background was not distracting and the wifi was working! Great job!",
                        Mark = 5,
                        Date = new DateTime(2017, 2, 13, 10, 38, 0),
                        TotalLikes = 3,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = "Nice place at the very center of the city, well decorated and clean. Coffee is excellent at a very reasonable prices. A lot of varieties of tea and cakes. Always a good option in Lviv!",
                        Mark = 4,
                        Date = new DateTime(2017, 1, 29, 19, 2, 0),
                        TotalLikes = 0,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = "Their coffee is consistent wherever you go. Helpful staff. They always have a nice variety of treats and souvenirs. They have a classic robust coffee flavor, but it's over roasted much like Starbucks. I don't think you will be disappointed, but you won't be amazed. Cheers,",
                        Mark = 3,
                        Date = new DateTime(2017, 1, 25, 13, 20, 0),
                        TotalLikes = -7,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = "If you visit Lviv you must to visit this factory. It is very interesting and different of others. At the same time you can enjoy a wonderful different coffees.",
                        Mark = 5,
                        Date = new DateTime(2017, 1, 19, 15, 16, 0),
                        TotalLikes = 4,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = "The place for the people who really liked the coffee ☕️! No jokes! If you are a person who takes the coffee at least once a day - you should visit the place named Coffee Manufacture and try out one of their special proportions! The coffee-fire-show will conquer you for sure)))",
                        Mark = 5,
                        Date = new DateTime(2017, 1, 18, 12, 56, 0),
                        TotalLikes = 1,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = "We came here for 15 minutes to drink coffee. It was wonderful, especially coffee with cinnamon and lemon. Next time we will definitely go and spend more time here!",
                        Mark = 4,
                        Date = new DateTime(2017, 1, 16, 19, 57, 0),
                        TotalLikes = 3,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = "Great location right on city square. MUST do tour of basement and order a coffee served with a blow torch to warm it.",
                        Mark = 5,
                        Date = new DateTime(2017, 1, 14, 14, 22, 0),
                        TotalLikes = 6,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = "One of the places where i take friends who come from other city to see Lviv. Cofffee is not the best in city but atmosphere is great and service is cool.",
                        Mark = 5,
                        Date = new DateTime(2016, 12, 26, 14, 0, 0),
                        TotalLikes = 2,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = "I enjoyed eating and drinking there, I even came back a few times. Every part of that place smelled amazing. There's a lot of different ways of drinking coffee here, they also have few interesting types of tea. And their ice cream is amazing! Staff (English-speaking) was really nice and we got our order quite fast.",
                        Mark = 5,
                        Date = new DateTime(2016, 12, 19, 9, 19, 0),
                        TotalLikes = 0,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = "The location is extraordinary with a cafe just build between two houses and a roof where you can see through!",
                        Mark = 5,
                        Date = new DateTime(2016, 10, 9, 15, 46, 0),
                        TotalLikes = 0,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = "Amazing place for coffee lovers. You can go downstairs to see the coffee mining process. They give you a special helmet with the light so that you can see what is happening around in spite of the dark atmosphere. You can find many touristic stuff to buy.",
                        Mark = 5,
                        Date = new DateTime(2016, 6, 10, 19, 51, 0),
                        TotalLikes = 0,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = "So nice place to drink many kind of coffee and to eat pastries.I highly commended to visit this place.",
                        Mark = 5,
                        Date = new DateTime(2016, 2, 10, 13, 24, 0),
                        TotalLikes = -1,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = "Nice coffee house with its own, unique atmosphere. One of 'must see' places in Lviv. U can drink a great coffee. U can go underground there and see how the coffee is being mined :D",
                        Mark = 5,
                        Date = new DateTime(2015, 11, 24, 19, 47, 0),
                        TotalLikes = 3,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = "There is a little cafe and a shop with gifts and different types of coffee you can buy according to the weight. But as for me the most interesting part was downstairs. There is a guest who can take you on a short tour to the 'manufacture', where you will be given a helmet (watch your head there!) and find some more places to seat and try coffee from a can which will be unsealed with a flame right in front of you!",
                        Mark = 5,
                        Date = new DateTime(2015, 1, 5, 20, 25, 0),
                        TotalLikes = 5,
                        Reviewer = reviewer
                    },
                    new Review
                    {
                        Description = @"The place is so popular among tourists (I'm not sure about locals) that the prices skyrocketed during the last year. That's why the rating is 4*, otherwise it would be 5*.

Be prepared to pay around UAH 100 for 3 coffee - based drinks - it's definitely much even by Lviv standards.

Espresso was good, capuccino was also nice as well as pastry.

Their roasted coffees are priced out of the world(around usd 40 per kilo) - one can easily find good roasted coffee at half the price in respectable roasting houses in the center of Lviv.

Visited January 2017",
                        Mark = 3,
                        Date = new DateTime(2014, 10, 15, 14, 31, 0),
                        TotalLikes = -9,
                        Reviewer = reviewer
                    }
                },
                Rating = 4.71,
                Owner = owner
            });

            Enterprises.Add(new Enterprise
            {
                Name = "Lviv's Chocolate Factory",
                Description = @"Since Medieval Times Lviv has been well known for its 'delicious confectionery' and starting from the ХІХ century Europe began exporting chocolate from Lviv. It wasn’t due to the best cocoa beans or some state-of-the-art technologies. The secret was in adding to a single sweet, a single bar of real chocolate a small, but very sweet, piece of their home city!

Lviv has always been able to work miracles.We use the same traditional production technology of old times and that is exactly why we have managed to revive the magical recipes giving life to our chocolates which again can travel around the world spreading the atmosphere of our beautiful city.And while you are opening a box with the 'chocolate' sign of Lviv Handmade Chocolate a smile will appear on your face. You can’t suppress your emotions, especially when they are about love as well as they come from Lviv!Enjoy our chocolate with moderation the first time!

Enjoy with pleasure!And leave some for tomorrow!",
                Address = new Address
                {
                    City = "Lviv",
                    Street = "Serbska vul.",
                    HouseNumber = "3"
                },
                Contacts = new List<string>
                {
                    "+380 50 430 6033"
                },
                Resources = new List<Resource>
                {
                    new Resource { DataPath = "0023.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0024.jpg", Type = ResourceType.SecondaryImage, Format = ResourceFormat.JPEG },
                    new Resource { DataPath = "0025.jpg", Type = ResourceType.MainImage, Format = ResourceFormat.JPEG }
                },
                Type = EnterpriceType.Restaurant,
                Reviews = new List<Review>
                {
                    new Review
                    {
                        Mark = 5,
                        Reviewer = reviewer,
                        Date = DateTime.Now,
                        TotalLikes = 3
                    },
                    new Review
                    {
                        Mark = 4,
                        Reviewer = reviewer,
                        Date = DateTime.Now,
                        TotalLikes = 3
                    },
                    new Review
                    {
                        Mark = 4,
                        Reviewer = reviewer,
                        Date = DateTime.Now,
                        TotalLikes = 3
                    }
                },
                Rating = 4.34,
                Owner = owner
            });

            Enterprises.Add(new Enterprise
            {
                Name = "Cukernia",
                Description = @"Аби досягнути досконалості, потрібен час та зосередженість на деталях. Наше солодке — це праця і досвід кількох поколінь, тисячі спроб, осяянь, ретельний добір — смаків, відтінків, ароматів, відчуттів.

Вечір за вечором, жінки, а інколи й чоловіки, шліфували свою майстерність у створенні того, що є таким самим атрибутом високої цивілізації, як парфуми чи примхливий дизайн, але пережило добу найсуворіших випробувань, було розрадою, надією та промінчиком щастя у роки лиха.

Ця майстерність у створенні солодкого дійшла до нас десь через сімейні традиції, десь — через старі зворушливі зошити з рецептами, й завдяки людям, які за будь-яких обставин намагалися зберігати гідність, раділи життю та наповнювали простір довкола себе досконалістю, навіть якщо єдиною можливою досконалістю був запах запечених у тісті яблук.",
                Address = new Address
                {
                    City = "Львів",
                    Street = "вул. Староєврейська",
                    HouseNumber = "3"
                },
                Contacts = new List<string>
                {
                    "e-mail: cukiernia.lviv@gmail.com",
                    "тел. (032) 235 69 49"
                },
                Resources = new List<Resource>
                {

                },
                Type = EnterpriceType.Restaurant,
                Reviews = new List<Review>
                {

                },
                Rating = 4.2,
                Owner = owner
            });           
            Enterprises.Add(new Enterprise
            {
                Name = "Cat Cafe",
                Description = @"Вітаємо Вас в Cat Cafe у Львові!

Ми розуміємо, що завести вдома кішку – це величезна відповідальність і не кожен має можливість та час на це. Тому раді Вас повідомити, що нарешті у Львові відкрито перший заклад у форматі Cat Cafe. Тут Ви маєте чудову нагоду за чашечкою запашної кави провести час в компанії самих різноманітних котів та кішок. Хочете зняти стрес, тривогу, нормалізувати тиск, відновити сили – тоді чекаємо на Вас в Cat Cafe! Тут ви можете поспілкуватись та побавитись з котиками, зробити чудові фото на пам’ять та подарувати тепло й увагу нашим маленьким улюбленцям.
Наші мешканці мають всі необхідні прививки, проходять регулярний ветеринарний огляд та перебувають під постійною увагою наших працівників.",
                Address = new Address
                {

                },
                Contacts = new List<string>
                {

                },
                Resources = new List<Resource>
                {

                },
                Type = EnterpriceType.Restaurant,
                Reviews = new List<Review>
                {

                },
                Rating = 4.9,
                Owner = owner
            });
            Enterprises.Add(new Enterprise
            {
                Address = new Address(),
                Name = "Svit Kavy",
                Description = @"Простора кав'ярня з широким асортиментом світової кави. До кави пропонуються різноманітні солодкі «лєґуміни» («ласощі» по-львівськи).

Кав'ярня пропонує скуштувати близько 30 сортів арабіки, різноманітні кавові коктейлі, львівські пляцки, соки, фреші, гарячий шоколад. 

У крамниці, що знаходиться поруч, можна придбати каву з собою та кавові аксесуари.",
                Rating = 3.2,
                Owner = owner
            });
            Enterprises.Add(new Enterprise
            {
                Address = new Address(),
                Name = "Cafe Centaur",
                Description = @"Кам‘яниця кафе «Centaur» береже пам’ять про своїх жителів ще з XVI ст. 1578 року тут поселився кушнір Авеншток – його ім’ям і названо було кам’яницю. На початку ХІХ ст. на площі Ринок, 34 розташувалася книгарня Міліковських. А вже 1847 року Людвік Штадтмюллер відкрив тут ресторан і торгівлю винами. Навпроти він тримав готель.",
                Rating = 3.7,
                Owner = owner
            });
            Enterprises.Add(new Enterprise
            {
                Address = new Address(),
                Name = "Something Interesting",
                Description = @"«Щось Цікаве» - це творчий простір у затишному дворику Львова. Домашня атмосфера, де вас раді бачити навіть о сьомій ранку :) Тут дозволяється експериментувати, говорити з незнайомцями та бути собою.

«Щось Цікаве» - це крамничка з художнім склом, де речі перестають бути речима. Тут природа і скло втілюються в оригінальні авторські прикраси, картини, світильники, в кожного з них своя історія, свій настрій, яким вони обов’язково поділяться з вами.

«Щось Цікаве» - це галерея просто неба з неймовірними історіями та сміливими ідеями.

«Щось Цікаве» - це місце, де приємно бачити, слухати, торкатися, пробувати, приміряти, і головне - відчувати. Все в одному місці: натхнення, поради, обмін досвідом, смачне какао та мамин сирник.
",
                Rating = 4.3,
                Owner = owner
            });
            Enterprises.Add(new Enterprise
            {
                Address = new Address(),
                Name = "Other Enterprise",
                Description = @"«Щось Цікаве» - це творчий простір у затишному дворику Львова. Домашня атмосфера, де вас раді бачити навіть о сьомій ранку :) Тут дозволяється експериментувати, говорити з незнайомцями та бути собою.

«Щось Цікаве» - це крамничка з художнім склом, де речі перестають бути речима. Тут природа і скло втілюються в оригінальні авторські прикраси, картини, світильники, в кожного з них своя історія, свій настрій, яким вони обов’язково поділяться з вами.",
                Rating = 2,
                Owner = owner
            });
            Enterprises.Add(new Enterprise
            {
                Address = new Address(),
                Name = "Other Enterprise",
                Description = @"«Щось Цікаве» - це творчий простір у затишному дворику Львова. Домашня атмосфера, де вас раді бачити навіть о сьомій ранку :) Тут дозволяється експериментувати, говорити з незнайомцями та бути собою.

«Щось Цікаве» - це крамничка з художнім склом, де речі перестають бути речима. Тут природа і скло втілюються в оригінальні авторські прикраси, картини, світильники, в кожного з них своя історія, свій настрій, яким вони обов’язково поділяться з вами.",
                Rating = 1.1,
                Owner = owner
            });
            Enterprises.Add(new Enterprise
            {
                Address = new Address(),
                Name = "Other Enterprise",
                Description = @"«Щось Цікаве» - це творчий простір у затишному дворику Львова. Домашня атмосфера, де вас раді бачити навіть о сьомій ранку :) Тут дозволяється експериментувати, говорити з незнайомцями та бути собою.

«Щось Цікаве» - це крамничка з художнім склом, де речі перестають бути речима. Тут природа і скло втілюються в оригінальні авторські прикраси, картини, світильники, в кожного з них своя історія, свій настрій, яким вони обов’язково поділяться з вами.",
                Rating = 1.3,
                Owner = owner
            });
            Enterprises.Add(new Enterprise
            {
                Address = new Address(),
                Name = "Other Enterprise",
                Description = @"«Щось Цікаве» - це творчий простір у затишному дворику Львова. Домашня атмосфера, де вас раді бачити навіть о сьомій ранку :) Тут дозволяється експериментувати, говорити з незнайомцями та бути собою.

«Щось Цікаве» - це крамничка з художнім склом, де речі перестають бути речима. Тут природа і скло втілюються в оригінальні авторські прикраси, картини, світильники, в кожного з них своя історія, свій настрій, яким вони обов’язково поділяться з вами.",
                Rating = 1.2,
                Owner = owner
            });
            Enterprises.Add(new Enterprise
            {
                Address = new Address(),
                Name = "Other Enterprise",
                Description = @"«Щось Цікаве» - це творчий простір у затишному дворику Львова. Домашня атмосфера, де вас раді бачити навіть о сьомій ранку :) Тут дозволяється експериментувати, говорити з незнайомцями та бути собою.

«Щось Цікаве» - це крамничка з художнім склом, де речі перестають бути речима. Тут природа і скло втілюються в оригінальні авторські прикраси, картини, світильники, в кожного з них своя історія, свій настрій, яким вони обов’язково поділяться з вами.",
                Rating = 1.5,
                Owner = owner
            });
            Enterprises.Add(new Enterprise
            {
                Address = new Address(),
                Name = "Other Enterprise",
                Description = @"«Щось Цікаве» - це творчий простір у затишному дворику Львова. Домашня атмосфера, де вас раді бачити навіть о сьомій ранку :) Тут дозволяється експериментувати, говорити з незнайомцями та бути собою.

«Щось Цікаве» - це крамничка з художнім склом, де речі перестають бути речима. Тут природа і скло втілюються в оригінальні авторські прикраси, картини, світильники, в кожного з них своя історія, свій настрій, яким вони обов’язково поділяться з вами.",
                Rating = 1.45,
                Owner = owner
            });
            Enterprises.Add(new Enterprise
            {
                Address = new Address(),
                Name = "Other Enterprise",
                Description = @"«Щось Цікаве» - це творчий простір у затишному дворику Львова. Домашня атмосфера, де вас раді бачити навіть о сьомій ранку :) Тут дозволяється експериментувати, говорити з незнайомцями та бути собою.

«Щось Цікаве» - це крамничка з художнім склом, де речі перестають бути речима. Тут природа і скло втілюються в оригінальні авторські прикраси, картини, світильники, в кожного з них своя історія, свій настрій, яким вони обов’язково поділяться з вами.",
                Rating = 1.67,
                Owner = owner
            });
            Enterprises.Add(new Enterprise
            {
                Address = new Address(),
                Name = "Other Enterprise",
                Description = @"«Щось Цікаве» - це творчий простір у затишному дворику Львова. Домашня атмосфера, де вас раді бачити навіть о сьомій ранку :) Тут дозволяється експериментувати, говорити з незнайомцями та бути собою.

«Щось Цікаве» - це крамничка з художнім склом, де речі перестають бути речима. Тут природа і скло втілюються в оригінальні авторські прикраси, картини, світильники, в кожного з них своя історія, свій настрій, яким вони обов’язково поділяться з вами.",
                Rating = 1.44,
                Owner = owner
            });
            Enterprises.Add(new Enterprise
            {
                Address = new Address(),
                Name = "Other Enterprise",
                Description = @"«Щось Цікаве» - це творчий простір у затишному дворику Львова. Домашня атмосфера, де вас раді бачити навіть о сьомій ранку :) Тут дозволяється експериментувати, говорити з незнайомцями та бути собою.

«Щось Цікаве» - це крамничка з художнім склом, де речі перестають бути речима. Тут природа і скло втілюються в оригінальні авторські прикраси, картини, світильники, в кожного з них своя історія, свій настрій, яким вони обов’язково поділяться з вами.",
                Rating = 1.90,
                Owner = owner
            });
            Enterprises.Add(new Enterprise
            {
                Address = new Address(),
                Name = "Other Enterprise",
                Description = @"«Щось Цікаве» - це творчий простір у затишному дворику Львова. Домашня атмосфера, де вас раді бачити навіть о сьомій ранку :) Тут дозволяється експериментувати, говорити з незнайомцями та бути собою.

«Щось Цікаве» - це крамничка з художнім склом, де речі перестають бути речима. Тут природа і скло втілюються в оригінальні авторські прикраси, картини, світильники, в кожного з них своя історія, свій настрій, яким вони обов’язково поділяться з вами.",
                Rating = 1.01,
                Owner = owner
            });
            Enterprises.Add(new Enterprise
            {
                Address = new Address(),
                Name = "Other Enterprise",
                Description = @"«Щось Цікаве» - це творчий простір у затишному дворику Львова. Домашня атмосфера, де вас раді бачити навіть о сьомій ранку :) Тут дозволяється експериментувати, говорити з незнайомцями та бути собою.

«Щось Цікаве» - це крамничка з художнім склом, де речі перестають бути речима. Тут природа і скло втілюються в оригінальні авторські прикраси, картини, світильники, в кожного з них своя історія, свій настрій, яким вони обов’язково поділяться з вами.",
                Rating = 1.99,
                Owner = owner
            });
            Enterprises.Add(new Enterprise
            {
                Address = new Address(),
                Name = "Other Enterprise",
                Description = @"«Щось Цікаве» - це творчий простір у затишному дворику Львова. Домашня атмосфера, де вас раді бачити навіть о сьомій ранку :) Тут дозволяється експериментувати, говорити з незнайомцями та бути собою.

«Щось Цікаве» - це крамничка з художнім склом, де речі перестають бути речима. Тут природа і скло втілюються в оригінальні авторські прикраси, картини, світильники, в кожного з них своя історія, свій настрій, яким вони обов’язково поділяться з вами.",
                Rating = 2.24,
                Owner = owner
            });
            Enterprises.Add(new Enterprise
            {
                Address = new Address(),
                Name = "Other Enterprise",
                Description = @"«Щось Цікаве» - це творчий простір у затишному дворику Львова. Домашня атмосфера, де вас раді бачити навіть о сьомій ранку :) Тут дозволяється експериментувати, говорити з незнайомцями та бути собою.

«Щось Цікаве» - це крамничка з художнім склом, де речі перестають бути речима. Тут природа і скло втілюються в оригінальні авторські прикраси, картини, світильники, в кожного з них своя історія, свій настрій, яким вони обов’язково поділяться з вами.",
                Rating = 2.51,
                Owner = owner
            });
            Enterprises.Add(new Enterprise
            {
                Address = new Address(),
                Name = "Other Enterprise",
                Description = @"«Щось Цікаве» - це творчий простір у затишному дворику Львова. Домашня атмосфера, де вас раді бачити навіть о сьомій ранку :) Тут дозволяється експериментувати, говорити з незнайомцями та бути собою.

«Щось Цікаве» - це крамничка з художнім склом, де речі перестають бути речима. Тут природа і скло втілюються в оригінальні авторські прикраси, картини, світильники, в кожного з них своя історія, свій настрій, яким вони обов’язково поділяться з вами.",
                Rating = 1.19,
                Owner = owner
            });           
            Enterprises.Add(new Enterprise
            {
                Address = new Address(),
                Name = "Other Enterprise",
                Description = @"«Щось Цікаве» - це творчий простір у затишному дворику Львова. Домашня атмосфера, де вас раді бачити навіть о сьомій ранку :) Тут дозволяється експериментувати, говорити з незнайомцями та бути собою.

«Щось Цікаве» - це крамничка з художнім склом, де речі перестають бути речима. Тут природа і скло втілюються в оригінальні авторські прикраси, картини, світильники, в кожного з них своя історія, свій настрій, яким вони обов’язково поділяться з вами.",
                Rating = 3.1,
                Owner = owner
            });         
            Enterprises.Add(new Enterprise
            {
                Address = new Address(),
                Name = "Other Enterprise",
                Description = @"«Щось Цікаве» - це творчий простір у затишному дворику Львова. Домашня атмосфера, де вас раді бачити навіть о сьомій ранку :) Тут дозволяється експериментувати, говорити з незнайомцями та бути собою.

«Щось Цікаве» - це крамничка з художнім склом, де речі перестають бути речима. Тут природа і скло втілюються в оригінальні авторські прикраси, картини, світильники, в кожного з них своя історія, свій настрій, яким вони обов’язково поділяться з вами.",
                Rating = 1.6,
                Owner = owner
            });

            foreach (Enterprise ent in Enterprises)
            {
                context.Enterprises.Add(ent);
            }

            #endregion

            base.Seed(context);
        }
    }


}
