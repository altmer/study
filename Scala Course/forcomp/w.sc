
import forcomp.Anagrams

object w {
  println("Welcome to the Scala worksheet")       //> Welcome to the Scala worksheet
                                                  

	Anagrams.sentenceAnagrams(List("Yes", "man"))
                                                  //> res0: List[forcomp.Anagrams.Sentence] = List(List(my, en, as), List(my, as, 
                                                  //| en), List(my, sane), List(my, Sean), List(yes, man), List(en, my, as), List(
                                                  //| en, as, my), List(men, say), List(as, my, en), List(as, en, my), List(say, m
                                                  //| en), List(man, yes), List(sane, my), List(Sean, my))
}