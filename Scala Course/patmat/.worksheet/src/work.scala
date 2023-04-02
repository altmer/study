
import patmat.Huffman

object work {import scala.runtime.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(62); val res$0 = 
	Huffman.decodedSecret;System.out.println("""res0: List[Char] = """ + $show(res$0));$skip(16); val res$1 = 
	Huffman.secret;System.out.println("""res1: List[patmat.Huffman.Bit] = """ + $show(res$1));$skip(23); val res$2 = 
	Huffman.encodedSecret;System.out.println("""res2: List[patmat.Huffman.Bit] = """ + $show(res$2));$skip(93); val res$3 = 
                                                  
  Huffman.secret == Huffman.encodedSecret;System.out.println("""res3: Boolean = """ + $show(res$3));$skip(32); val res$4 = 
  
	Huffman.encodedSecretQuick;System.out.println("""res4: List[patmat.Huffman.Bit] = """ + $show(res$4));$skip(47); val res$5 = 
  Huffman.secret == Huffman.encodedSecretQuick;System.out.println("""res5: Boolean = """ + $show(res$5))}
}