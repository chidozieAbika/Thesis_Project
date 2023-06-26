using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Motte_IT.Models;
namespace Motte_IT.Data
{
   public class priorityqueue
    {
        private ClientComplaint[] heap;   
        public int lastindex;

       public priorityqueue(int size)
        {
            this.heap = new ClientComplaint[size];
            this.lastindex = 0;

        }

        private void swimup()
        {
            int index = lastindex;  // the last index might not be the last index so send it to variable intex aka temp
            int indexparent = parent(index);

            while (indexparent > 0 && heap[index].KeyValue > heap[indexparent].KeyValue) {
                swap(index, indexparent);
                index = indexparent;
                indexparent = parent(indexparent);
            
            }

        }

       

        private void swimdown()
        {

            int index = 1; // root

            while (index < lastindex) 
            {

                int maxvalue = heap[index].KeyValue;  // parent?
                int maxindex = index;

                int leftindex = lefttchild(index);
                if(leftindex > 0 && maxvalue < heap[leftindex].KeyValue)  // if left is bigger than 0 and the max value is smaller than left child
                {
                    maxvalue = heap[leftindex].KeyValue;
                    maxindex = lefttchild(index);
                }

                int rightindex = rightchild(index);
                if (rightindex > 0 && maxvalue < heap[rightindex].KeyValue)  // if left is bigger than 0 and the max value is smaller than left child
                {
                    maxvalue = heap[rightindex].KeyValue;
                    maxindex = lefttchild(index);
                }

                if(maxindex == index)
                {
                    break;
                    
                }
                    swap(maxindex, index);
                    index = maxindex;
            }

        }

        private int parent(int index) {             // return the parental node index

            if (index <= 1) {   

                return 0;
            }
            else { return index / 2; }
        }

        private int rightchild(int index) {

            int right = index * 2 + 1;
            return right <= lastindex ? right : 0;   

        }
        private int lefttchild(int index)
        {

            int left = index * 2 ;
            return left <= lastindex ? left : 0;   
        }

        private void swap(int index1, int index2) 
        {

            ClientComplaint temp = heap[index1];
            heap[index1] = heap[index2]; 
            heap[index2] = temp;

        }

      public  void enqueue(ClientComplaint value) 
        { 
            if(lastindex + 1 > heap.Length - 1)
            {
                ClientComplaint[] temppheap = heap;                     // dynamic
                heap = new ClientComplaint[temppheap.Length * 2];
                for (int i = 0; i < temppheap.Length; i++) {

                    heap[i] = temppheap[i];
                }
            }
            // metod                
            setKeyValue(value);


            lastindex++;
            heap[lastindex] = value;
            swimup();
        }

   public   ClientComplaint dequeue()
        {
            if (lastindex <= 0) {
                return null;
            }
            ClientComplaint rootvalue = heap[1];
            swap(1, lastindex);
            lastindex--;
            swimdown();
            return rootvalue;

        }

         void setKeyValue(ClientComplaint x) {

            x.KeyValue = 0;
            switch(x.CategorySubject)
            {
                case    "Keyboard":
                         x.KeyValue = 1;
                         break;
                case    "Mouse":
                         x.KeyValue = 2;
                         break;
                case    "other computer accessories":
                        x.KeyValue = 3;
                        break;
                case    "Screen":
                        x.KeyValue = 4;
                         break;
                case    "Network":
                        x.KeyValue = 5;
                         break;
                case    "Programm":
                        x.KeyValue = 6;
                         break;
                default:
                    break;
            }

        }
    }
    
}
