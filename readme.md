# 🐍 Snake Game – Tugas Proyek Grafika Komputer 2025

## 🧾 Informasi Umum
- **Nama:** Hidayat Lossen  
- **NIM:** 2300018116  
- **Mata Kuliah:** Grafika Komputer  
- **Tahun Akademik:** 2025  
- **Judul Proyek:** Game Snake  
- **Platform Pengembangan:** Unity  
- **Versi Unity:** 2022.3.62f1 (LTS)

## 📝 Deskripsi Proyek
Proyek ini merupakan pembuatan game **Snake** klasik sebagai bagian dari tugas mata kuliah **Grafika Komputer**. Game dikembangkan menggunakan **Unity Engine**, dengan kontrol sederhana dan sistem pertumbuhan tubuh ular saat memakan makanan.

## 🎮 Fitur Utama
- Kontrol ular dengan tombol `W`, `A`, `S`, `D` atau tombol panah.
- Posisi awal ular dan arah gerak ditentukan secara **acak** saat game dimulai.
- Setiap kali ular memakan makanan, tubuhnya bertambah panjang.
- Makanan muncul secara acak dan **tidak pernah muncul di atas tubuh ular**.
- Menggunakan **object pooling** untuk segmen tubuh ular agar efisien memori.
- Permainan akan di-reset jika ular menabrak tubuhnya sendiri atau penghalang.

## 🧱 Tools & Teknologi
- **Unity 2022.3.62f1 LTS**
- **Bahasa Pemrograman:** C#
- **Sprite Renderer** untuk menampilkan objek
- **Rigidbody2D dan Collider2D** untuk deteksi tabrakan
- **Object Pooling** untuk efisiensi performa
- **Unity Profiler** digunakan untuk memantau penggunaan memori dan performa runtime

## 🗂️ Struktur Folder Penting (Untuk Repositori Git)

```
Tp_Grafika_Snake_2025/
├── Assets/
│   ├── Resources/       # (Opsional, bisa kosong)
│   ├── Scenes/          # Scene utama permainan
│   ├── Scripts/         # Script Snake.cs, Food.cs, dll.
│   └── Sprites/         # Sprite ular, makanan, background
├── Packages/            # Info dependencies Unity
├── ProjectSettings/     # Pengaturan Unity project
└── README.md
```



## 🧠 Catatan Tambahan
- Project ini bisa dikembangkan lebih lanjut dengan fitur seperti skor, efek suara, sistem level, dan UI game over.
- Semua aset gambar yang digunakan bersifat **gratis** dan bebas digunakan untuk keperluan pendidikan.

## 🎯 Tujuan Proyek
Game ini dibuat untuk menerapkan berbagai konsep grafika komputer, seperti:
- Transformasi posisi objek dalam ruang 2D
- Deteksi tabrakan (collision detection)
- Manajemen sprite & animasi sederhana
- Randomisasi posisi objek
- Optimisasi performa dengan object pooling

---

Terima kasih telah membaca dokumentasi ini!  
Semoga game Snake ini dapat menjadi referensi yang berguna untuk pembelajaran dan tugas lainnya.
