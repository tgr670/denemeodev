using Microsoft.EntityFrameworkCore;

namespace denemeodev
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnderssec_Click(object sender, EventArgs e)
        {
            using (var context = new OkulContext())
            {
                var ogrenci = context.Ogrenciler.Include(o => o.OgrenciDersleri).FirstOrDefault(o => o.Numara == tboxNo.Text);

                if (ogrenci != null)
                {
                    
                    var dersSecimForm = new Form2();
                    dersSecimForm.SecilenOgrenciId = ogrenci.OgrenciId; 
                    dersSecimForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Lütfen önce bir öðrenci bulun!");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var context = new OkulContext())
            {
                cboxsýnýf.DataSource = context.Siniflar.ToList();
                cboxsýnýf.DisplayMember = "Adi";
                cboxsýnýf.ValueMember = "SinifId";
            }
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            using (var context = new OkulContext())
            {
                string adi = tboxad.Text.Trim();
                string soyadi = tboxsoyad.Text.Trim();
                string numara = tboxNo.Text.Trim();

                try
                {
                    
                    if (cboxsýnýf.SelectedValue == null)
                    {
                        MessageBox.Show("Lütfen bir sýnýf seçin.");
                        return;
                    }

                    int sinifId = (int)cboxsýnýf.SelectedValue;
                    var sinif = context.Siniflar.Include(s => s.Ogrenciler).FirstOrDefault(s => s.SinifId == sinifId);

                    if (sinif == null)
                    {
                        MessageBox.Show("Seçilen sýnýf bulunamadý.");
                        return;
                    }

                    var mevcutOgrenci = context.Ogrenciler.FirstOrDefault(o => o.Adi == adi && o.Soyadi == soyadi && o.Numara == numara);
                    if (mevcutOgrenci != null)
                    {
                        MessageBox.Show("Bu öðrenci zaten kayýtlý!");
                        return;
                    }

                    
                    if (sinif.Ogrenciler.Count >= sinif.Kapasite)
                    {
                        MessageBox.Show("Seçilen sýnýfýn kapasitesi dolmuþ!");
                        return;
                    }

                    
                    if (string.IsNullOrWhiteSpace(tboxad.Text) || string.IsNullOrWhiteSpace(tboxsoyad.Text) || string.IsNullOrWhiteSpace(tboxNo.Text))
                    {
                        MessageBox.Show("Lütfen tüm alanlarý doldurun.");
                        return;
                    }

                    var yeniOgrenci = new Ogrenci
                    {
                        Adi = tboxad.Text,
                        Soyadi = tboxsoyad.Text,
                        Numara = tboxNo.Text,
                        SinifId = sinifId
                    };

                    context.Ogrenciler.Add(yeniOgrenci);
                    context.SaveChanges();

                    MessageBox.Show("Öðrenci baþarýyla kaydedildi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluþtu: {ex.Message}");
                }
            }
        }

        private void btnbul_Click(object sender, EventArgs e)
        {
            using (var context = new OkulContext())
            {
                int numara;
                if (int.TryParse(tboxNo.Text, out numara))
                {
                    var ogrenci = context.Ogrenciler.Include(o => o.Sinif).FirstOrDefault(o => o.Numara == tboxNo.Text);

                    if (ogrenci != null)
                    {
                        tboxad.Text = ogrenci.Adi;
                        tboxsoyad.Text = ogrenci.Soyadi;
                        cboxsýnýf.SelectedValue = ogrenci.SinifId;

                        MessageBox.Show("Öðrenci bilgileri yüklendi.");
                    }
                    else
                    {
                        MessageBox.Show("Öðrenci bulunamadý.");
                    }
                }
                else
                {
                    MessageBox.Show("Geçerli bir öðrenci numarasý girin.");
                }
            }
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            using (var context = new OkulContext())
            {
                int numara;
                if (int.TryParse(tboxNo.Text, out numara))
                {
                    var ogrenci = context.Ogrenciler.FirstOrDefault(o => o.Numara == tboxNo.Text);

                    if (ogrenci != null)
                    {
                        ogrenci.Adi = tboxad.Text;
                        ogrenci.Soyadi = tboxsoyad.Text;
                        ogrenci.SinifId = (int)cboxsýnýf.SelectedValue;

                        context.SaveChanges();
                        MessageBox.Show("Öðrenci bilgileri baþarýyla güncellendi.");
                    }
                    else
                    {
                        MessageBox.Show("Öðrenci bulunamadý.");
                    }
                }
                else
                {
                    MessageBox.Show("Geçerli bir öðrenci numarasý girin.");
                }
            }
        }
    }
}
