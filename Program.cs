using System.Collections;

//Meios de prolongar a vida útil de um objeto grande
//*Maneira incorreta*//

ArrayList list = new(85190);
UseList(list);

list = new(85190);
UseList(list);

//*Maneira correta*//

ArrayList list2 = new(85190);
UseList(list2);

list2.Clear();
UseList(list2);

//!Explicação! 
//em vez de criar uma nova instância de um ArrayList, utilizar a mesma instância, assim prologando a vida útil de um objeto que inicialmente é definido como "Large"

static void UseList(ArrayList obj){

}

//Meios de encurtar a vida útil de um objeto pequeno
//*Maneira incorreta*//

ArrayList list3 = new(85190);

for (int i=0; i<1000; i++){
    list3.Add(new Pair(i, i+1));
}

//*Maneira correta*//

int[] list_1 = new int[85190];
int[] list_2 = new int[85190];

for(int i=0; i<1000; i++){
    list_1[i] = i;
    list_2[i] = i + 1;
}

//!Explicação!
//Dentro do ArrayList é criado uma nova instância da classe Pair para cada elemento da lista, portanto ela nuncaria irá perder sua referência, e sempre será mandado para seguinte gerações.
//Já o array é uma coleção mais performática e é baseada é uma struct int, um tipo primitivo. significa que é criado uma instância única que delimita o tamanho, e é armazenado no Large heap tipos valores

//Meios de refatoração de um objeto grande para um objeto pequeno
//*Maneira incorreta*//

int[] buffer = new int[32768];

for(int i=0; i>buffer.Length; i++){
    buffer[i] = GetByte(i);
}

//*Maneira correta*//
byte[] buffer_2 = new byte[32768];

for(int i=0; i>1000; i++){
    buffer[i] = GetByte(i);
}

//!Explicação!
// O problema é definir um array de inteiros para armazenar bytes, devido um inteiro corresponder a 4x o valor de um byte. então ele ultrapassará o valor que o GC utiliza como parâmetro para alocar no LOH
// Como solução, criar um array de bytes é o indicado para armazenar bytes. assim ele será definido na gen0

//Meios de refatoração de um objeto pequeno para um objeto grande
//*Maneira incorreta*//

Array.list = new();
UseList(Array.list);

//*Maneira correta*//

Array.list = new(85190);
UseList(Array.list);

//!Explicação!//
//Devido a a lista ser definido como estática, acredita-se será um objeto de vida longa. Já que será um objeto de vida longa, em vez de inicializar com o valor default de um ArrayList que é 16Kilobytes
//É recomendável inicializar com um valor  acima de 85190bytes, para forçar a alocação do objeto no LOH

static byte GetByte(int value){
    byte valueB = (byte) value;
    return valueB;
}
public class Pair{

    public double Value1 {get; set;}
    public double Value2 {get; set;}

    public Pair(double value1, double value2){
    Value1 = value1;
    Value2 = value2;

}
}
public static class Array{
    public static ArrayList list {get; set;} = new();
}



    

