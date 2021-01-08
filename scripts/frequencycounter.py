wordfile = open(".\\dictionary.txt", "r")
counts = []
total = 0
alphabet = "abcdefghijklmnopqrstuvwxyz"
normcounts = []


for i in range(len(alphabet)):
    counts.append(0)
    normcounts.append(0)

#print (counts)

for line in wordfile:
    for letter in line:
        if letter in alphabet:
            total += 1
            counts[alphabet.index(letter)] += 1

wordfile.close()
#print( counts)

outfile = open("normletterfrequencies.txt", "a")

for i in range(len(counts)):
    normcounts[i] = float(counts[i]) / total
    outfile.write(str(normcounts[i]) + "\n")
print( normcounts)
outfile.close()