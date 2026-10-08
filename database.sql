CREATE DATABASE IF NOT EXISTS medisistem_db 
  CHARACTER SET utf8mb4 
  COLLATE utf8mb4_general_ci;


USE medisistem_db;

-- Kullanıcılar tablosu (giriş için)
CREATE TABLE IF NOT EXISTS users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    email VARCHAR(100) NOT NULL UNIQUE,
    -- PBKDF2-SHA256 özeti: PBKDF2-SHA256$<iterasyon>$<tuz>$<özet> (~90 karakter)
    password VARCHAR(128) NOT NULL,
    adsoyad VARCHAR(100)
);

-- Örnek kullanıcı (demo şifresi README'de; ilk girişten sonra değiştirin)
INSERT INTO users (email, password, adsoyad)
VALUES ('doctor@example.com',
        'PBKDF2-SHA256$100000$mgVzdawLEJT1qmFQzllDtw==$clDkn8zZufD4DjaBVyJozSXOObYWWdlNGGbFGWUFrag=',
        'Dr. Ahmet Yılmaz');


--  Hastalar tablosu
CREATE TABLE IF NOT EXISTS patients (
    id INT AUTO_INCREMENT PRIMARY KEY,
    tc_no VARCHAR(11),
    adsoyad VARCHAR(100),
    telefon VARCHAR(20),
    tani VARCHAR(255),
    doktor VARCHAR(100),
    durum VARCHAR(50),
    son_ziyaret DATE
);

-- Örnek hasta kayıtları 
INSERT INTO patients (tc_no, adsoyad, telefon, tani, doktor, durum, son_ziyaret) VALUES
('12345678901', 'Ahmet Yılmaz', '0555 123 45 67', 'Hipertansiyon', 'Dr. Ayşe Yılmaz', 'Tedavi Görüyor', '2023-11-15'),
('23456789012', 'Ayşe Demir',   '0532 987 65 43', 'Diyabet Tip 2', 'Dr. Mehmet Arslan', 'Randevu Bekliyor', '2023-10-20'),
('34567890123', 'Mehmet Kaya',  '0542 111 22 33', 'Grip',          'Dr. Ali Veli',   'Taburcu Edildi',   '2023-11-25');
