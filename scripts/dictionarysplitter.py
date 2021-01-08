wordfile = open(".\\dictionary.txt", "r")
words = []
lencounts = []
for line in wordfile:
	words.append(line)
	lencounts.append(len(line))
wordfile.close()

print words[0]
wordlists = []
for i in range(min(lencounts)-1,max(lencounts)):
        wordlists.append([])
        
for word in words:
    wordlists[len(word)-3].append(word)

    
for i in range(len(wordlists)):
    writefile = open(str(i+2) + "letterwords.txt", "w")
    for word in wordlists[i]:
        writefile.write(word)
    writefile.close()
