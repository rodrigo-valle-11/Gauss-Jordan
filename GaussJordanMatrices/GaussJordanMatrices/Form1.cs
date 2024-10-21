using System;
using System.Drawing;
using System.Windows.Forms;

namespace GaussJordanMatrices
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            // Llenar el ComboBox con opciones de tamaño de matriz
            comboBox1.Items.Add("1x1");
            comboBox1.Items.Add("2x2");
            comboBox1.Items.Add("3x3");
            comboBox1.Items.Add("4x4");

            // Seleccionar por defecto la opción 1x1
            comboBox1.SelectedIndex = 0;

            // Generar la matriz inicial (1x1)
            SetMatrixSize(1);
        }
        private void GaussJordan(double[,] matrix, int size)
        {
            for (int i = 0; i < size; i++)
            {
                // Hacer el elemento principal igual a 1
                double divisor = matrix[i, i];
                for (int j = 0; j < size + 1; j++)
                {
                    matrix[i, j] /= divisor;
                }

                // Hacer ceros los demás elementos de la columna
                for (int k = 0; k < size; k++)
                {
                    if (k != i)
                    {
                        double factor = matrix[k, i];
                        for (int j = 0; j < size + 1; j++)
                        {
                            matrix[k, j] -= factor * matrix[i, j];
                        }
                    }
                }
            }

            // Mostrar el resultado
            string result = "Solución:\n";
            for (int i = 0; i < size; i++)
            {
                result += $"x{i + 1} = {matrix[i, size]}\n";
            }
            MessageBox.Show(result);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Obtener el tamaño de la matriz
            int size = comboBox1.SelectedIndex + 1;

            // Crear una matriz de tamaño [size, size+1] para almacenar los coeficientes
            double[,] matrix = new double[size, size + 1];

            // Llenar la matriz con los valores del DataGridView
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size + 1; j++)
                {
                    matrix[i, j] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                }

                button1.FlatStyle = FlatStyle.Flat;
                

            }

            // Aplicar el método de Gauss-Jordan
            GaussJordan(matrix, size);

            // Mostrar los resultados en un TextBox o donde prefieras
            string result = "";
            for (int i = 0; i < size; i++)
            {
                result += $"x{i + 1} = {matrix[i, size]}\r\n";  // Mostrar las soluciones
            }

            textBox1.Text = result;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el tamaño de la matriz seleccionado (1x1, 2x2, etc.)
            int size = comboBox1.SelectedIndex + 1;

            // Ajustar el DataGridView según el tamaño seleccionado
            SetMatrixSize(size);
        }
        private void SetMatrixSize(int size)
        {
            // Configurar el número de filas y columnas
            dataGridView1.RowCount = size;
            dataGridView1.ColumnCount = size + 1; // Columna adicional para el resultado

            // Ajustar los encabezados de las columnas (opcionales)
            for (int i = 0; i < size; i++)
            {
                dataGridView1.Columns[i].HeaderText = "x" + (i + 1);  // Encabezados de las variables
            }
            dataGridView1.Columns[size].HeaderText = "Resultado";  // Encabezado para el término independiente
        }

        // Método para resolver la matriz usando el método de Gauss-Jordan
        private void SolveGaussJordan(double[,] matrix, int size)
        {
            for (int i = 0; i < size; i++)
            {
                // Asegurarse de que el pivote no sea 0, si lo es intercambiar filas
                if (matrix[i, i] == 0)
                {
                    bool swapped = false;
                    for (int k = i + 1; k < size; k++)
                    {
                        if (matrix[k, i] != 0)
                        {
                            SwapRows(matrix, i, k, size);
                            swapped = true;
                            break;
                        }
                    }
                    if (!swapped)
                    {
                        MessageBox.Show("No se puede resolver el sistema: hay una fila de ceros.");
                        return;
                    }
                }

                // Hacer que el pivote sea 1
                double pivot = matrix[i, i];
                for (int j = 0; j < size + 1; j++)
                {
                    matrix[i, j] /= pivot;
                }

                // Hacer ceros los elementos de la columna actual, excepto el pivote
                for (int k = 0; k < size; k++)
                {
                    if (k != i)
                    {
                        double factor = matrix[k, i];
                        for (int j = 0; j < size + 1; j++)
                        {
                            matrix[k, j] -= factor * matrix[i, j];
                        }
                    }
                }
            }
        }
        // Método para intercambiar dos filas de la matriz
        private void SwapRows(double[,] matrix, int row1, int row2, int size)
        {
            for (int i = 0; i < size + 1; i++)
            {
                double temp = matrix[row1, i];
                matrix[row1, i] = matrix[row2, i];
                matrix[row2, i] = temp;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;


        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            // Ajustar el estilo del DataGridView
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;

            // Ajustar el tamaño del formulario y el TextBox
            this.Font = new Font("Segoe UI", 10);
            textBox1.Multiline = true;  // Para que se vea mejor el TextBox
            textBox1.Height = 50;

            // Estilo de los botones
            button1.FlatStyle = FlatStyle.Flat;
            button2.FlatStyle = FlatStyle.Flat;

            // Otros ajustes que ya tenías
            comboBox1.Items.Add("1x1");
            comboBox1.Items.Add("2x2");
            comboBox1.Items.Add("3x3");
            comboBox1.Items.Add("4x4");
            comboBox1.SelectedIndex = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Limpiar el contenido del DataGridView
            dataGridView1.Rows.Clear();

            // foreach (DataGridViewRow row in dataGridView1.Rows)
            // {
            //     foreach (DataGridViewCell cell in row.Cells)
            //     {
            //         cell.Value = null; // Borra los valores de cada celda
            //     }
            // }

            // Limpiar el contenido del TextBox de resultados
            textBox1.Clear();

            // Reiniciar la selección en el ComboBox a 1x1 (opcional)
            comboBox1.SelectedIndex = 0;

            // Ajustar el tamaño de la matriz a 1x1 por defecto (opcional)
            SetMatrixSize(1);
        }
    }
}
        

    

